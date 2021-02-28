using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_MVVM_Base.Converters
{
    public class BoolInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            throw new Exception("value must be of type boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            throw new Exception("value must be of type boolean");
        }
    }
}