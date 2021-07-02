using System;
using System.Globalization;
using System.Windows.Data;

namespace lab10
{
    public class WidthHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = (double)value;
            double param = double.Parse(parameter.ToString());
            return val / param;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
