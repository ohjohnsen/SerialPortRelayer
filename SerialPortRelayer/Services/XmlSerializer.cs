using NLog;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortRelayer.Services {
    public class XmlSerializer {

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private const string FileName = "config.xml";

        public static bool DisableXmlSerializer { get; set; }

        public static void LoadFromFile(Controller controller) {
            DisableXmlSerializer = true;
            try {
                using (var stream = new FileStream(FileName, FileMode.Open)) {
                    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTemplate));
                    var xmlTemplate = (XmlTemplate)serializer.Deserialize(stream);
                    stream.Close();
                    SetValues(controller, xmlTemplate);
                    _logger.Debug("Configuration loaded from file '{0}'", FileName);
                }
            }
            catch (FileNotFoundException ex) {
                // config.xml file not found  ->  Using default settings instead
                // TODO: Add default settings
                System.Windows.MessageBox.Show(ex.Message);
                _logger.Debug("Configuration file '{0}' not found -> Loading default configuration", FileName);
                SetDefault(controller);
                SaveToFile(controller);
            }
            catch (Exception ex) {
                // some other exception
                System.Windows.MessageBox.Show(ex.ToString());
            }
            DisableXmlSerializer = false;
        }

        private static void SetDefault(Controller controller) {
            controller.SerialPortHandler.SelectedBaudRate = 38400;
        }

        public static void SaveToFile(Controller controller) {
            if (DisableXmlSerializer == false) {
                try {
                    using (var stream = new FileStream(FileName, FileMode.Create)) {
                        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(XmlTemplate));
                        var xmlTemplate = GetValues(controller);
                        serializer.Serialize(stream, xmlTemplate);
                        stream.Close();
                        _logger.Debug("Configuration saved to file '{0}'", FileName);
                    }
                }
                catch (Exception ex) {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
        }

        private static XmlTemplate GetValues(Controller controller) {
            var template = new XmlTemplate();
            template.RelayerPortAName = controller.SerialPortHandler.SelectedRelayerPortAName;
            template.RelayerPortBName = controller.SerialPortHandler.SelectedRelayerPortBName;
            template.SnifferPortABName = controller.SerialPortHandler.SelectedSnifferPortABName;
            template.SnifferPortBAName = controller.SerialPortHandler.SelectedSnifferPortBAName;
            template.BaudRate = controller.SerialPortHandler.SelectedBaudRate;
            return template;
        }

        private static void SetValues(Controller controller, XmlTemplate xmlTemplate) {
            var serialPortList = SerialPortStream.GetPortNames();

            if (serialPortList.Contains(xmlTemplate.RelayerPortAName)) {
                controller.SerialPortHandler.SelectedRelayerPortAName = xmlTemplate.RelayerPortAName;
            }

            if (serialPortList.Contains(xmlTemplate.RelayerPortBName)) {
                controller.SerialPortHandler.SelectedRelayerPortBName = xmlTemplate.RelayerPortBName;
            }

            if (serialPortList.Contains(xmlTemplate.SnifferPortABName)) {
                controller.SerialPortHandler.SelectedSnifferPortABName = xmlTemplate.SnifferPortABName;
            }

            if (serialPortList.Contains(xmlTemplate.SnifferPortBAName)) {
                controller.SerialPortHandler.SelectedSnifferPortBAName = xmlTemplate.SnifferPortBAName;
            }

            controller.SerialPortHandler.SelectedBaudRate = xmlTemplate.BaudRate;
        }
    }
}
