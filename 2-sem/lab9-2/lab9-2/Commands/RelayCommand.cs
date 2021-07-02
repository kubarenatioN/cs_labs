using System;
using System.Windows.Input;

namespace lab9
{
    public class RelayCommand : ICommand
    {
        private Action execute;
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action exec)
        {
            execute = exec;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
