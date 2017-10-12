using SerialPortRelayer.Services;
using SerialPortRelayer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortRelayer.Views {
    public class MainWindowVM : BindableBase {

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

        private MainWindow _mainWindow;
        public MainWindow MainWindow {
            get { return _mainWindow; }
            set { SetNotify(ref _mainWindow, value); }
        }

        private void LoadCommands() {

        }
    }
}
