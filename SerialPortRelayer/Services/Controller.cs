using SerialPortRelayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortRelayer.Services {

    public class Controller {

        public Controller() {

        }

        public MainWindowVM MainWindowVM { get; set; }

        public SerialPortHandler SerialPortHandler { get; set; }
    }
}
