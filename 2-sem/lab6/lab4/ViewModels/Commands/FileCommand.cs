using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace lab4.ViewModels.Commands
{
    public class FileCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
        public Key GestureKey { get; set; }
        public ModifierKeys GestureModifier { get; set; }
        public KeyGesture Shortcut { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public FileCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public FileCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public FileCommand(Action<object> execute, Key key, ModifierKeys modifier, Func<object, bool> canExecute = null) : this(execute, canExecute)
        {
            GestureKey = key;
            GestureModifier = modifier;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute.Invoke(parameter);
        }
    }
}
