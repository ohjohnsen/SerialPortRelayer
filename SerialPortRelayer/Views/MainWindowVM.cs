using SerialPortRelayer.Services;
using SerialPortRelayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerialPortRelayer.Views {
    public class MainWindowVM : BindableBase {

        private const string WINDOW_TITLE = "Serial Port Relayer v";
        private Version _version = Assembly.GetEntryAssembly().GetName().Version;

        private Controller _controller;

        public Controller Controller {
            get { return _controller; }
            set { SetNotify(ref _controller, value); }
        }

        public MainWindowVM(Controller controller, MainWindow mainWindow) {
            _controller = controller;
            _mainWindow = mainWindow;

            LoadCommands();
        }

        public string WindowTitle {
            get { return WINDOW_TITLE + _version.Major + "." + _version.Minor + "." + _version.Build; }
        }

        private MainWindow _mainWindow;
        public MainWindow MainWindow {
            get { return _mainWindow; }
            set { SetNotify(ref _mainWindow, value); }
        }

        public ICommand OpenSerialPortsCommand { get; set; }
        public ICommand CloseSerialPortsCommand { get; set; }

        private void LoadCommands() {
            OpenSerialPortsCommand = new CustomCommand(_controller.SerialPortHandler.OpenSerialPorts, _controller.SerialPortHandler.CanOpenSerialPorts);
            CloseSerialPortsCommand = new CustomCommand(_controller.SerialPortHandler.CloseSerialPorts, _controller.SerialPortHandler.CanCloseSerialPorts);
        }
    }
}
