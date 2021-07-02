﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab4.ViewModels.Commands;
using Microsoft.Win32;
using System.Windows.Controls;
using System.IO;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace lab4.ViewModels
{
    public class FileDialogViewModel
    {
        public Window MainWindow { get; set; }
        public System.Windows.Controls.RichTextBox TextEditor { get; set; }
        private RelayCommand openFile;
        public RelayCommand OpenFile
        {
            get
            {
                return openFile ?? (openFile = new RelayCommand(obj =>
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
                    fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (fileDialog.ShowDialog() == true)
                    {

                        FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open);
                        TextRange range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
                        range.Load(fileStream, DataFormats.Rtf);
                        fileStream.Close();
                        MainWindow.Title = fileDialog.FileName;

                    //TextEditor.Document.Blocks.Clear();
                    //    TextEditor.Document.Blocks.Add(new Paragraph(new Run(File.ReadAllText(fileDialog.FileName))));
                    }
                }));
            }
        }

        private RelayCommand saveFile;
        public RelayCommand SaveFile
        {
            get
            {
                return saveFile ?? (saveFile = new RelayCommand(obj =>
                {
                    SaveFileDialog fileDialog = new SaveFileDialog();
                    fileDialog.Filter = "Text files (*.txt*)|*.txt*|Rich Text Format (*.rtf)|*.rtf";
                    fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (fileDialog.ShowDialog() == true)
                    {
                        FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Create);
                        TextRange range = new TextRange(TextEditor.Document.ContentStart, TextEditor.Document.ContentEnd);
                        range.Save(fileStream, DataFormats.Text);
                        fileStream.Close();
                        MainWindow.Title = fileDialog.FileName;
                    }
                }));
            }
        }

        private RelayCommand newFile;
        public RelayCommand NewFile
        {
            get
            {
                return newFile ?? (newFile = new RelayCommand(obj =>
                {
                    //Clear all text
                    TextEditor.Document.Blocks.Clear();
                    // add new paragraph
                    TextEditor.Document.Blocks.Add(new Paragraph());
                    // find grid container
                    Grid grid = TextEditor.Parent as Grid;
                    // refresh initial font slider value
                    Extensions.GetChildOfType<Slider>(grid).Value = 14;
                    Extensions.GetChildOfType<ComboBox>(grid).SelectedIndex = 0;
                    Extensions.GetChildOfType<ColorPicker>(grid).SelectedColor = Color.FromArgb(255, 0, 0, 0);
                    MainWindow.Title = (string)Application.Current.Resources["mainWindowTitle"];
                }, Key.N, ModifierKeys.Control));
            }
        }

        
        public FileDialogViewModel()
        {

        }
    }

    public static class Extensions
    {
        public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
