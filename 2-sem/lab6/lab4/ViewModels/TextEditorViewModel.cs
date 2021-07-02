using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace lab4.ViewModels
{
    public class TextEditorViewModel : INotifyPropertyChanged
    {
        public RichTextBox TextEditor { get; set; }

        private string symbolNumber = "0";
        public string SymbolsNumber
        {
            get { return symbolNumber; }
            set
            {
                symbolNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SymbolsNumber)));
            }
        }
        private string wordsNumber = "0";
        public string WordsNumber
        {
            get { return wordsNumber; }
            set
            {
                wordsNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(WordsNumber)));
            }
        }
        public TextEditorViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
