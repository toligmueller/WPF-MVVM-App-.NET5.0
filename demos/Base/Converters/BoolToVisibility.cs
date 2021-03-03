using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_MVVM_Base.Converters
{
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    return Visibility.Visible;
                }                
                return Visibility.Collapsed;
            }
            throw new Exception("value must be of type boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                if ((Visibility)value == Visibility.Visible)
                {
                    return true;
                }
                return false;
            }
            throw new Exception("value must be of type visibility");
        }
    }
}