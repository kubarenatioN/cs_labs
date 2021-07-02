using lab4.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace lab4.ViewModels
{
    public class FontFamilyViewModel
    {
        private FontFamilyChangeCommand ChangeFamilyCommand;
        public RichTextBox TextEditor { get; set; }
        public Slider FontSizeSlider { get; set; }
        public Dictionary<int, string> Families { get; set; }
        private int selectedItemId;
        public int SelectedItemId
        {
            get { return selectedItemId; }
            set
            {
                selectedItemId = value;
                ChangeFamilyCommand.Execute(null);
            }
        }

        public FontFamilyViewModel()
        {
            Families = new Dictionary<int, string>();
            Families.Add(0, "Segoe UI");
            Families.Add(1, "Arial");
            Families.Add(2, "Times New Roman");
            Families.Add(3, "Century Gothic");
            Families.Add(4, "Lucida Console");
            ChangeFamilyCommand = new FontFamilyChangeCommand(FontFamilyChange);
        }

        public void FontFamilyChange()
        {
            if (TextEditor == null) return;
            //if (!TextEditor.Selection.IsEmpty)
            //{
            //    TextEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, Families[SelectedItemId]);
            //}
            //else
            //{
            //    TextEditor.FontFamily = new FontFamily(Families[SelectedItemId]);
            //}
            RichTextBox target = TextEditor;
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
                        newRun.FontFamily = new FontFamily(Families[SelectedItemId]);
                        newRun.FontSize = FontSizeSlider.Value;
                        curParagraph.Inlines.Add(newRun);
                        target.CaretPosition = newRun.ElementStart;
                        target.Focus();
                    }
                    else if (endRange.IsEmpty || string.IsNullOrWhiteSpace(endRange.Text))
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
                            newRun.FontFamily = new FontFamily(Families[SelectedItemId]);
                            newRun.FontSize = FontSizeSlider.Value;
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
                    selectionTextRange.ApplyPropertyValue(TextElement.FontFamilyProperty, Families[SelectedItemId]);
                }
            }
        }
    }
}
