using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SerialPortRelayer.Utilities {

    class CustomCommand : ICommand {

        private Action<object> _execute;
        private Predicate<object> _canExecute;
        private Action openSerialPorts;
        private Func<object, bool> canOpenSerialPorts;

        public CustomCommand(Action<object> execute, Predicate<object> canExecute) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public CustomCommand(Action openSerialPorts, Func<object, bool> canOpenSerialPorts) {
            this.openSerialPorts = openSerialPorts;
            this.canOpenSerialPorts = canOpenSerialPorts;
        }

        public bool CanExecute(object parameter) {
            bool b = _canExecute == null ? true : _canExecute(parameter);
            return b;
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add {
                CommandManager.RequerySuggested += value;
            }
            remove {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
