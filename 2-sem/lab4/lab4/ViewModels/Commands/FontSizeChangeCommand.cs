using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab4.ViewModels.Commands
{
    public class FontSizeChangeCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<RichTextBox> method;

        
        public FontSizeChangeCommand(Action<RichTextBox> m)
        {
            method = m;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            method.Invoke((RichTextBox)parameter);
        }
    }
}
