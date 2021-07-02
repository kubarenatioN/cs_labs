using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class SearchComboBoxItem
    {
        public string Text { get; set; }
        public string Parameter { get; set; }
        public ComboBoxItemType Type { get; set; }
        public SearchComboBoxItem(string text, string param, ComboBoxItemType type)
        {
            Text = text;
            Parameter = param;
            Type = type;
        }
        public override string ToString()
        {
            return Text;
        }
    }
    public enum ComboBoxItemType
    {
        Letters,
        Digits
    }
}
