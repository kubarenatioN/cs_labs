using lab4.ViewModels;
using lab4.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace lab4
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Window mainWindow;
        public event PropertyChangedEventHandler PropertyChanged;
        private string currentTime;
        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                if (currentTime == value) return;
                currentTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentTime)));
            }
        }

        private List<MenuItem> themeMenuItems = new List<MenuItem>();
        public List<MenuItem> ThemeMenuItems
        {
            get { return themeMenuItems; }
            set
            {
                themeMenuItems = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(ThemeMenuItems)));
            }
        }
        private string currentThemeName = "-light";

        private List<MenuItem> recentFiles = new List<MenuItem>();
        public List<MenuItem> RecentFiles
        {
            get
            {
                recentFiles.Reverse();
                return recentFiles;
            }
            set
            {
                recentFiles = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(RecentFiles)));
            }
        }

        private List<MenuItem> cultures = new List<MenuItem>();
        public List<MenuItem> Cultures
        {
            get { return cultures; }
            set
            {
                cultures = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Cultures)));
            }
        }

        //private RelayCommand changeThemeCommand;
        public RelayCommand ChangeThemeCommand { get; set; }
        public RelayCommand OpenFileCommand { get; set; }
        public RelayCommand ChangeCultureCommand { get; set; }

        public MainWindowViewModel(Window w)
        {
            mainWindow = w;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += SetTime;
            timer.Start();

            ChangeThemeCommand = new RelayCommand(ChangeTheme);

            OpenFileCommand = new RelayCommand((path) =>
            {
                FileStream fileStream = new FileStream((string)path, FileMode.Open);
                FileDialogViewModel fileDialogViewModel = (mainWindow.TryFindResource("fileDialog") as FileDialogViewModel);
                TextRange range = new TextRange(fileDialogViewModel.TextEditor.Document.ContentStart, fileDialogViewModel.TextEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
                fileStream.Close();
                mainWindow.Title = (string)path;
            });

            ChangeCultureCommand = new RelayCommand(ChangeCulture);

            MenuItem themeItemLight = new MenuItem()
            {
                Header = Resources.Localization.LightThemeMenuItem,
                Tag = "-light",
                IsChecked = true
            };
            MenuItem themeItemDark = new MenuItem()
            {
                Tag = "-dark",
                Header = Resources.Localization.DarkThemeMenuItem
            };
            MenuItem themeItemBlue = new MenuItem()
            {
                Tag = "-blue",
                Header = Resources.Localization.BlueThemeMenuItem,
            };
            ThemeMenuItems.Add(themeItemLight);
            ThemeMenuItems.Add(themeItemDark);
            ThemeMenuItems.Add(themeItemBlue);
            foreach (MenuItem item in ThemeMenuItems)
            {
                item.IsCheckable = true;
                item.Command = ChangeThemeCommand;
                item.CommandParameter = item;
            }

            MenuItem ruCulture = new MenuItem()
            {
                Header = Resources.Localization.LangRuMenuItem,
                Tag = "ru-RU",
                //IsChecked = true
            };
            MenuItem enCulture = new MenuItem()
            {
                Header = Resources.Localization.LangEnMenuItem,
                Tag = "en-EN"
            };
            Cultures.Add(ruCulture);
            Cultures.Add(enCulture);
            foreach (MenuItem item in Cultures)
            {
                item.IsCheckable = true;
                item.Command = ChangeCultureCommand;
                item.CommandParameter = item;
                if((string)item.Tag == Thread.CurrentThread.CurrentCulture.Name)
                {
                    item.IsChecked = true;
                }
            }

            LoadRecentFilePaths();
        }

        public void ChangeCulture(object item)
        {
            MenuItem curItem = item as MenuItem;
            foreach (MenuItem m in Cultures)
            {
                m.IsChecked = false;
            }
            curItem.IsChecked = true;
            string cultureName = (string)curItem.Tag;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);

            Window oldWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();

            oldWindow.Close();

            Console.WriteLine(Resources.Localization.DisableEditorMenuItem);
        }

        public void LoadRecentFilePaths()
        {
            using (StreamReader reader = new StreamReader(@"D:\Visual_Studio\2 course\2-sem\lab6\lab4\recent-files.txt"))
            {
                int row = 0;
                string line = null;
                while(row++ < 10 && (line = reader.ReadLine()) != null)
                {
                    MenuItem m = new MenuItem()
                    {
                        Header = line,
                        Command = OpenFileCommand,
                        CommandParameter = line
                    };
                    RecentFiles.Add(m);
                }
            }
        }

        public void ChangeTheme(object item)
        {
            foreach (MenuItem m in ThemeMenuItems)
            {
                m.IsChecked = false;
            }
            MenuItem activeItem = item as MenuItem;
            activeItem.IsChecked = true;

            string newThemeName = (string)activeItem.Tag;

            Uri currentThemeUri = new Uri($"Styles/Colors{currentThemeName}.xaml", UriKind.Relative);

            ICollection<ResourceDictionary> resDicts = Application.Current.Resources.MergedDictionaries;
            ResourceDictionary currentThemeDict = resDicts.FirstOrDefault(d => d.Source == currentThemeUri);

            int index = Application.Current.Resources.MergedDictionaries.IndexOf(currentThemeDict);

            ResourceDictionary newThemeDict = new ResourceDictionary()
            {
                Source = new Uri($"Styles/Colors{newThemeName}.xaml", UriKind.Relative)
            };

            Application.Current.Resources.MergedDictionaries.RemoveAt(index);
            Application.Current.Resources.MergedDictionaries.Insert(index, newThemeDict);
            currentThemeName = newThemeName;
        }

        public void SetTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLongTimeString();
        }

    }
}
