using System;
using System.Windows.Input;

namespace lab10
{
    public class RelayCommand : ICommand
    {
        private Action _exec;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute)
        {
            _exec = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _exec.Invoke();
        }
    }
}
