﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace lab7
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ProductCardControl_ButtonClick(object sender, RoutedEventArgs e)
        {
            ProductCardControl card = sender as ProductCardControl;
            card.CompositionOpacity = card.CompositionOpacity == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;

            Console.WriteLine(sender.ToString());
            Console.WriteLine(e.Source);
        }
    }
}
