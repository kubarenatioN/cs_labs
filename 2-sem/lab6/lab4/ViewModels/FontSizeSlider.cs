using lab4.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace lab4.ViewModels
{
    public class FontSizeSlider
    {
        public FontSizeChangeCommand ChangeFontCommand { get; set; }
        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value != _value) _value = value;
            }
        }
        public bool CanChangeFontSize;
        public ComboBox FontFamilyCombobox{get;set;}

        public FontSizeSlider()
        {
            ChangeFontCommand = new FontSizeChangeCommand(FontSizeChange);
        }

        public void FontSizeChange(RichTextBox target)
        {
            if (target.Selection != null)
            {
                // Check whether there is text selected or just sitting at cursor
                if (target.Selection.IsEmpty)
                {
                    TextPointer tp = target.GetPositionFromPoint(new Point(0, 0), true);
                    //string FirstParagraphLine = ((Run)tp.Paragraph.Inlines.FirstInline).Text;
                    TextRange endRange = new TextRange(target.CaretPosition, target.Document.ContentEnd);
                    if (target.Selection.Start.IsAtLineStartPosition)
                    {
                        Paragraph curParagraph = target.Document.Blocks.FirstBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        newRun.FontSize = Value;
                        newRun.FontFamily = new FontFamily((string)FontFamilyCombobox.SelectedValue);
                        curParagraph.Inlines.Add(newRun);
                        target.CaretPosition = newRun.ElementStart;
                        target.Focus();
                    }
                    else if(endRange.IsEmpty || string.IsNullOrWhiteSpace(endRange.Text))
                    {
                        // Get current position of cursor
                        TextPointer curCaret = target.CaretPosition;
                        // Get the current block object that the cursor is in
                        Block curBlock = target.Document.Blocks.Where
                            (x => x.ContentStart.CompareTo(curCaret) == -1 && x.ContentEnd.CompareTo(curCaret) == 1).FirstOrDefault();
                        if (curBlock != null)
                        {
                            Paragraph curParagraph = curBlock as Paragraph;
                            // Create a new run object with the fontsize, and add it to the current block
                            Run newRun = new Run();
                            newRun.FontSize = Value;
                            newRun.FontFamily = new FontFamily((string)FontFamilyCombobox.SelectedValue);
                            curParagraph.Inlines.Add(newRun);
                            // Reset the cursor into the new block. 
                            // If we don't do this, the font size will default again when you start typing.
                            target.CaretPosition = newRun.ElementStart;
                        }
                        // Reset the focus onto the richtextbox after selecting the font in a toolbar etc
                        target.Focus();
                    }
                }
                else // There is selected text, so change the fontsize of the selection
                {
                    TextRange selectionTextRange = new TextRange(target.Selection.Start, target.Selection.End);
                    selectionTextRange.ApplyPropertyValue(TextElement.FontSizeProperty, (double)Value);
                }
            }
        }
    }
}
