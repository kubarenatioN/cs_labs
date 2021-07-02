using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab4.ViewModels.Commands
{
    class FontFamilyChangeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;
        private Func<bool> _canExecute = null;

        public FontFamilyChangeCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public FontFamilyChangeCommand(Action execute) : this(execute, null)
        {

        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
