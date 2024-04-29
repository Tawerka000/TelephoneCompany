using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TelephoneCompany.Services
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> executeAction { get; set; }
        private Predicate<object> canExecute { get; set; }
        public RelayCommand(Action<object> executeMethod, Predicate<object> canExecuteMethod)
        {
            executeAction = executeMethod;
            canExecute = canExecuteMethod;
        }


        public bool CanExecute(object parameter)
        {
            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
