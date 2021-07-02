using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab4.ViewModels.Commands
{
    class FontColorChangeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;

        public FontColorChangeCommand(Action execute)
        {
            _execute = execute;
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
