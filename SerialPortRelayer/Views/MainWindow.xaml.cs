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

        private MainWindowVM _mainWindowVM;

        public MainWindow() {
            var controller = new Controller();
            _mainWindowVM = new MainWindowVM(controller, this);
            DataContext = _mainWindowVM;

            InitializeComponent();
        }
    }
}
