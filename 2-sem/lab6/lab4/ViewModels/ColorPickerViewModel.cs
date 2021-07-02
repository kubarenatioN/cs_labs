using lab4.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace lab4.ViewModels
{
    public class ColorPickerViewModel
    {
        public RichTextBox TextEditor { get; set; }
        private Color colorCode;
        private FontColorChangeCommand colorCommand;
        public Color ColorCode
        {
            get { return colorCode; }
            set
            {
                if (colorCode == value) return;
                colorCode = value;
                colorCommand.Execute(null);
            }
        }

        public ColorPickerViewModel()
        {
            colorCommand = new FontColorChangeCommand(ChangeColor);
        }

        private void ChangeColor()
        {
            if (TextEditor == null) return;
            if (!TextEditor.Selection.IsEmpty)
            {
                TextEditor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, ColorCode.ToString());
            }
            else
            {
                TextEditor.Foreground = new SolidColorBrush(ColorCode);
            }
        }
    }
}
