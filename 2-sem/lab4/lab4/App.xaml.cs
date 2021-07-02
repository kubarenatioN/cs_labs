using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<CultureInfo> languages = new List<CultureInfo>();
        public static List<CultureInfo> Languages
        {
            get
            {
                return languages;
            }
        }

        public App()
        {
            languages.Clear();
            languages.Add(new CultureInfo("en-US"));//Добавляем культуры
            languages.Add(new CultureInfo("ru-RU"));
        }

        public static event EventHandler LanguageChanged;
        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("Null Value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;//Change app lang

                // Create resource dictionary for new Culture
                ResourceDictionary dictionary = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dictionary.Source = new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative);
                        break;
                    default:
                        dictionary.Source = new Uri($"Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                //Get old dictionary
                ResourceDictionary oldDictionary = Application.Current.Resources.MergedDictionaries
                    .Where(d => d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang."))
                    .Select(d => d).First();

                if(oldDictionary != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldDictionary);

                    //Remove old dictionary
                    Application.Current.Resources.MergedDictionaries.Remove(oldDictionary);
                    //Add new dictionary at index
                    Application.Current.Resources.MergedDictionaries.Insert(index, dictionary);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dictionary);
                }

                // Fire event that culture was changed
                LanguageChanged(Application.Current, new EventArgs());
            }
        }
    }
}
