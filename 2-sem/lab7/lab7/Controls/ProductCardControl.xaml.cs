using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace lab7
{
    /// <summary>
    /// Логика взаимодействия для ProductCardControl.xaml
    /// </summary>
    public partial class ProductCardControl : UserControl, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ProductCardControl), 
                new PropertyMetadata("test sushi...", null, new CoerceValueCallback(CorrectTitleValue)), 
                new ValidateValueCallback(ValidateTitleValue));


        public string Composition
        {
            get { return (string)GetValue(CompositionProperty); }
            set { SetValue(CompositionProperty, value); }
        }
        
        public static readonly DependencyProperty CompositionProperty =
            DependencyProperty.Register("Composition", typeof(string), typeof(ProductCardControl), new PropertyMetadata("Default composition placeholder..."));


        public int Amount
        {
            get { return (int)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }
        
        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register("Amount", typeof(int), typeof(ProductCardControl),
                new PropertyMetadata(1), 
                new ValidateValueCallback(ValidateAmountValue));


        public Uri ImageSource
        {
            get { return (Uri)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(Uri), typeof(ProductCardControl), new PropertyMetadata(new Uri("../Images/osaka.png", UriKind.Relative)), 
                new ValidateValueCallback(ValidateImageSourceValue));


        private static bool ValidateTitleValue(object title)
        {
            if (title.ToString().ToLower().Contains("sushi")) return true;
            return false;
        }

        private static object CorrectTitleValue(DependencyObject d, object baseValue)
        {
            string title = baseValue.ToString();
            if (title.EndsWith(".")) return title.Replace(".", "");
            else return title;
        }

        private static bool ValidateAmountValue(object amount)
        {
            if ((int)amount >= 0) return true;
            return false;
        }

        private static bool ValidateImageSourceValue(object source)
        {
            if (source.ToString().EndsWith(".png") || source.ToString().EndsWith(".jpg")) return true;
            return false;
        }

        private double sum;
        public double Sum
        {
            get { return sum; }
            set
            {
                if (sum == value) return;
                sum = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Sum"));
            }
        }

        private double price;
        public double Price
        {
            get { return (double)GetValue(PriceProperty); }
            set
            {
                SetValue(PriceProperty, value);
            }
        }
        
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(double), typeof(ProductCardControl), new PropertyMetadata(0.0), new ValidateValueCallback(ValidatePriceValue));

        private static bool ValidatePriceValue(object price)
        {
            if (double.Parse(price.ToString()) >= 0) return true;
            return false;
        }
        private static object CorrectPriceValue(DependencyObject d, object basePrice)
        {
            double price = double.Parse(basePrice.ToString());
            if (price != (int)price) return Math.Round(price, 1);
            else return price;
        }

        public Visibility compositionOpacity = Visibility.Collapsed;
        public Visibility CompositionOpacity
        {
            get
            {
                return compositionOpacity;
            }
            set
            {
                compositionOpacity = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CompositionOpacity)));
            }
        }

        public static RoutedEvent MyClickEvent = EventManager.RegisterRoutedEvent("MyClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ProductCardControl));

        public event RoutedEventHandler MyClick
        {
            add { AddHandler(MyClickEvent, value); }
            remove { RemoveHandler(MyClickEvent, value); }
        }

        void RaiseClickEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MyClickEvent);
            RaiseEvent(newEventArgs);
        }

        private static readonly RoutedEvent MouseLostFocusEvent = EventManager.RegisterRoutedEvent("MouseLostFocus", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(IconButtonControl));

        public event RoutedEventHandler MouseLostFocus
        {
            add { AddHandler(MouseLostFocusEvent, value); }
            remove { RemoveHandler(MouseLostFocusEvent, value); }
        }
        void RaiseMouseLostFocusEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MouseLostFocusEvent);
            RaiseEvent(newEventArgs);
        }


        public static RoutedCommand IconAddCommand = new RoutedCommand();
        public static RoutedCommand IconRemoveCommand = new RoutedCommand();

        public ProductCardControl()
        {
            InitializeComponent();

            MouseLeftButtonDown += (sender, e) => RaiseClickEvent();
            MouseLeave += (sender, e) => RaiseMouseLostFocusEvent();

            DataContext = this;
        }
        public void ProductCardControl_ButtonClick(object sender, RoutedEventArgs e)
        {
            ProductCardControl card = sender as ProductCardControl;
            card.CompositionOpacity = card.CompositionOpacity == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
        private void IconButtonControl_MouseHover(object sender, RoutedEventArgs e)
        {
            IconButtonControl icon = sender as IconButtonControl;
            icon.IconFill = new SolidColorBrush(Colors.Violet);

            Console.WriteLine(sender);
            Console.WriteLine(e.Source);

        }
        private void IconButtonControl_MouseLeave(object sender, RoutedEventArgs e)
        {
            IconButtonControl icon = sender as IconButtonControl;
            icon.IconFill = new SolidColorBrush(Colors.White);

            Console.WriteLine(sender);
            Console.WriteLine(e.Source);
        }

        public void IconAddCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Amount != 100) Amount++;
            else Amount = 100;
            Sum = Price * Amount;
        }
        public void IconRemoveCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (Amount != 1) Amount--;
            else Amount = 1;
            Sum = Price * Amount;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Sum = Price;

        }
    }
}
