using NLog;
using RJCP.IO.Ports;
using SerialPortRelayer.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortRelayer.Services {
    public class SerialPortHandler : BindableBase {

        private Controller _controller;
        private SerialPortStream _relayerPortA;
        private SerialPortStream _relayerPortB;
        private SerialPortStream _snifferPortAB;
        private SerialPortStream _snifferPortBA;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public SerialPortHandler(Controller controller) {
            _controller = controller;

            _relayerPortA = new SerialPortStream();
            _relayerPortA.DataBits = 8;
            _relayerPortA.StopBits = StopBits.One;
            _relayerPortA.Parity = Parity.None;
            _relayerPortA.Handshake = Handshake.None;

            _relayerPortB = new SerialPortStream();
            _relayerPortB.DataBits = 8;
            _relayerPortB.StopBits = StopBits.One;
            _relayerPortB.Parity = Parity.None;
            _relayerPortB.Handshake = Handshake.None;

            _snifferPortAB = new SerialPortStream();
            _snifferPortAB.DataBits = 8;
            _snifferPortAB.StopBits = StopBits.One;
            _snifferPortAB.Parity = Parity.None;
            _snifferPortAB.Handshake = Handshake.None;

            _snifferPortBA = new SerialPortStream();
            _snifferPortBA.DataBits = 8;
            _snifferPortBA.StopBits = StopBits.One;
            _snifferPortBA.Parity = Parity.None;
            _snifferPortBA.Handshake = Handshake.None;
        }

        public ObservableCollection<string> SerialPortList {
            get { return GetSerialPortList(); }
        }

        private string _selectedRelayerPortAName;
        public string SelectedRelayerPortAName {
            get { return _selectedRelayerPortAName; }
            set {
                SetNotify(ref _selectedRelayerPortAName, value);
                _relayerPortA.PortName = SelectedRelayerPortAName;
                _logger.Info("Relayer Port A set to {0}", SelectedRelayerPortAName);
                XmlSerializer.SaveToFile(_controller);
            }
        }

        private string _selectedRelayerPortBName;
        public string SelectedRelayerPortBName {
            get { return _selectedRelayerPortBName; }
            set {
                SetNotify(ref _selectedRelayerPortBName, value);
                _relayerPortB.PortName = SelectedRelayerPortBName;
                _logger.Info("Relayer Port B set to {0}", SelectedRelayerPortBName);
                XmlSerializer.SaveToFile(_controller);
            }
        }

        private string _selectedSnifferPortABName;
        public string SelectedSnifferPortABName {
            get { return _selectedSnifferPortABName; }
            set {
                SetNotify(ref _selectedSnifferPortABName, value);
                _snifferPortAB.PortName = SelectedSnifferPortABName;
                _logger.Info("Sniffer Port A->B set to {0}", SelectedSnifferPortABName);
                XmlSerializer.SaveToFile(_controller);
            }
        }

        private string _selectedSnifferPortBAName;
        public string SelectedSnifferPortBAName {
            get { return _selectedSnifferPortBAName; }
            set {
                SetNotify(ref _selectedSnifferPortBAName, value);
                _snifferPortBA.PortName = SelectedSnifferPortBAName;
                _logger.Info("Sniffer Port B->A set to {0}", SelectedSnifferPortBAName);
                XmlSerializer.SaveToFile(_controller);
            }
        }

        public ObservableCollection<int> BaudRateList {
            get { return new ObservableCollection<int> { 9600, 19200, 38400, 57600, 115200 }; }
        }

        private int _selectedBaudRate;
        public int SelectedBaudRate {
            get { return _selectedBaudRate; }
            set {
                SetNotify(ref _selectedBaudRate, value);
                _relayerPortA.BaudRate = SelectedBaudRate;
                _relayerPortB.BaudRate = SelectedBaudRate;
                _snifferPortAB.BaudRate = SelectedBaudRate;
                _snifferPortBA.BaudRate = SelectedBaudRate;
                XmlSerializer.SaveToFile(_controller);
            }
        }

        public ObservableCollection<string> GetSerialPortList() {
            var serialPortList = new ObservableCollection<string>();
            string[] serialPortArray = SerialPortStream.GetPortNames();
            Array.Sort(serialPortArray, delegate (string a, string b) {  // Sort COM port list according to "n" in "COMn"
                return (int.Parse(a.Substring(3)).CompareTo(int.Parse(b.Substring(3))));
            });
            foreach (string s in serialPortArray) {
                serialPortList.Add(s);
            }
            return serialPortList;
        }
    }
}
