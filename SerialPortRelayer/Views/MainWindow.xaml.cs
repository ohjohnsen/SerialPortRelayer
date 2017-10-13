using NLog;
using SerialPortRelayer.Services;
using SerialPortRelayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerialPortRelayer.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MainWindow() {
            var controller = new Controller();

            var serialPortHandler = new SerialPortHandler(controller);
            controller.SerialPortHandler = serialPortHandler;

            var mainWindowVM = new MainWindowVM(controller, this);
            controller.MainWindowVM = mainWindowVM;

            DataContext = mainWindowVM;

            InitializeComponent();

            XmlSerializer.LoadFromFile(controller);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            _logger.Debug("Application started");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            _logger.Debug("Application closing");
        }
    }
}
