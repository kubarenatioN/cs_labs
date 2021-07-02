using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace lab7
{
    /// <summary>
    /// Логика взаимодействия для IconButtonControl.xaml
    /// </summary>
    public partial class IconButtonControl : UserControl
    {
        public Geometry IconPath
        {
            get { return (Geometry)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        public static readonly DependencyProperty IconPathProperty =
            DependencyProperty.Register("IconPath", typeof(Geometry), typeof(IconButtonControl), new PropertyMetadata());
        

        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }
        
        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush), typeof(IconButtonControl), new PropertyMetadata(Brushes.White), new ValidateValueCallback(ValidateIconFillValue));

        public static bool ValidateIconFillValue(object iconFill)
        {
            SolidColorBrush brush = iconFill as SolidColorBrush;

            if (brush != null) return true;
            return false;
        }


        private static readonly RoutedEvent MouseHoverEvent = EventManager.RegisterRoutedEvent("MouseHover", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(IconButtonControl));

        public event RoutedEventHandler MouseHover
        {
            add { AddHandler(MouseHoverEvent, value); }
            remove { RemoveHandler(MouseHoverEvent, value); }
        }

        void RaiseMouseHoverEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(MouseHoverEvent);
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


        public RoutedCommand Command
        {
            get { return (RoutedCommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(RoutedCommand), typeof(IconButtonControl), new PropertyMetadata());


        public IconButtonControl()
        {
            InitializeComponent();

            MouseEnter += (sender, e) => RaiseMouseHoverEvent();
            MouseLeave += (sender, e) => RaiseMouseLostFocusEvent();
            //MouseLeftButtonDown += (sender, e) => RaiseIconClickEvent();

            DataContext = this;
        }
    }
}
