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

            SelectedBaudRate = 38400;
        }

        private SerialPortStream _relayerPortA;
        public SerialPortStream RelayerPortA {
            get { return _relayerPortA; }
            set { SetNotify(ref _relayerPortA, value); }
        }

        private SerialPortStream _relayerPortB;
        public SerialPortStream RelayerPortB {
            get { return _relayerPortB; }
            set { SetNotify(ref _relayerPortB, value); }
        }

        private SerialPortStream _snifferPortAB;
        public SerialPortStream SnifferPortAB {
            get { return _snifferPortAB; }
            set { SetNotify(ref _snifferPortAB, value); }
        }

        private SerialPortStream _snifferPortBA;
        public SerialPortStream SnifferPortBA {
            get { return _snifferPortBA; }
            set { SetNotify(ref _snifferPortBA, value); }
        }

        public ObservableCollection<string> SerialPortList {
            get { return GetSerialPortList(); }
        }

        private string _selectedRelayerPortAName;
        public string SelectedRelayerPortAName {
            get { return _selectedRelayerPortAName; }
            set {
                SetNotify(ref _selectedRelayerPortAName, value);
                RelayerPortA.PortName = SelectedRelayerPortAName;
            }
        }

        private string _selectedRelayerPortBName;
        public string SelectedRelayerPortBName {
            get { return _selectedRelayerPortBName; }
            set {
                SetNotify(ref _selectedRelayerPortBName, value);
                RelayerPortB.PortName = SelectedRelayerPortBName;
            }
        }

        private string _selectedSnifferPortABName;
        public string SelectedSnifferPortABName {
            get { return _selectedSnifferPortABName; }
            set {
                SetNotify(ref _selectedSnifferPortABName, value);
                SnifferPortAB.PortName = SelectedSnifferPortABName;
            }
        }

        private string _selectedSnifferPortBAName;
        public string SelectedSnifferPortBAName {
            get { return _selectedSnifferPortBAName; }
            set {
                SetNotify(ref _selectedSnifferPortBAName, value);
                SnifferPortBA.PortName = SelectedSnifferPortBAName;
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
                RelayerPortA.BaudRate = SelectedBaudRate;
                RelayerPortB.BaudRate = SelectedBaudRate;
                SnifferPortAB.BaudRate = SelectedBaudRate;
                SnifferPortBA.BaudRate = SelectedBaudRate;
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
