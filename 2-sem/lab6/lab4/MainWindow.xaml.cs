using lab4.Properties;
using lab4.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FontFamilyViewModel fontFamilyVM = (FontFamilyViewModel)fontFamilyComboBox.DataContext;
            fontFamilyVM.TextEditor = richTextBox;
            fontFamilyVM.FontSizeSlider = fontSizeSlider;

            ColorPickerViewModel colorPickerVM = (ColorPickerViewModel)colorPicker.DataContext;
            colorPickerVM.TextEditor = richTextBox;

            FileDialogViewModel fileDialogVM = (FileDialogViewModel)fileDialogMenuItem.DataContext;
            fileDialogVM.TextEditor = richTextBox;
            fileDialogVM.MainWindow = this;

            //App.LanguageChanged += LanguageChanged;
            //CultureInfo currentLang = App.Language;
            ////Refresh language menu
            //LangMenu.Items.Clear();
            //foreach (CultureInfo lang in App.Languages)
            //{
            //    MenuItem item = new MenuItem();
            //    item.Header = lang.DisplayName;
            //    item.Tag = lang;
            //    item.IsChecked = lang.Equals(currentLang);
            //    item.Click += LanguageChange_Click;
            //    LangMenu.Items.Add(item);
            //}
            //App.Language = currentLang;

            DataContext = new MainWindowViewModel(this);

            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            //Console.WriteLine(lab4.Resources.Localization.FileMenuItem);
        }

        private bool fontSizeLocker = true;
        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (richTextBox != null && fontSizeLocker)
            {
                Slider slider = (sender as Slider); 
                FontSizeSlider fontSizeSlider = (FontSizeSlider)slider.DataContext;
                fontSizeSlider.FontFamilyCombobox = fontFamilyComboBox;
                fontSizeSlider.ChangeFontCommand.Execute(richTextBox);
            }
        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {

        }

        private void RichTextBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] docPath = (string[])e.Data.GetData(DataFormats.FileDrop);

                // By default, open as Rich Text (RTF).
                var dataFormat = DataFormats.Rtf;

                // If the Shift key is pressed, open as plain text.
                if (e.KeyStates == DragDropKeyStates.ShiftKey)
                {
                    dataFormat = DataFormats.Text;
                }

                TextRange range;
                System.IO.FileStream fStream;
                if (System.IO.File.Exists(docPath[0]))
                {
                    try
                    {
                        // Open the document in the RichTextBox.
                        range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                        fStream = new System.IO.FileStream(docPath[0], System.IO.FileMode.OpenOrCreate);
                        range.Load(fStream, dataFormat);
                        fStream.Close();
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("File could not be opened. Make sure the file is a text file.");
                    }
                }
            }
        }

        private void RichTextBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = false;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextEditorViewModel textEditor = (TextEditorViewModel)(sender as RichTextBox).DataContext;
            TextRange textRange = new TextRange(
                // TextPointer to the start of content in the RichTextBox.
                richTextBox.Document.ContentStart,
                // TextPointer to the end of content in the RichTextBox.
                richTextBox.Document.ContentEnd
            );

            int n = 0;
            foreach (Match match in Regex.Matches(textRange.Text, @"[^\n\t\r]+"))
            {
                n += match.Length;
            }
            textEditor.SymbolsNumber = n.ToString();

            textEditor.WordsNumber = Regex.Matches(textRange.Text, @"\b(\w+)\b").Count.ToString();
        }

        private void CommandBinding_CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void LanguageChanged(object sender, EventArgs e)
        {
            CultureInfo currentLang = App.Language;

            foreach (MenuItem item in LangMenu.Items)
            {
                CultureInfo cultureInfo = item.Tag as CultureInfo;
                item.IsChecked = cultureInfo != null && cultureInfo.Equals(currentLang);
            }
        }

        private void LanguageChange_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if(item != null)
            {
                CultureInfo lang = item.Tag as CultureInfo;
                if(lang != null)
                {
                    App.Language = lang;
                    Title = (string)Application.Current.Resources["mainWindowTitle"];
                }
            }
        }
    }
}
