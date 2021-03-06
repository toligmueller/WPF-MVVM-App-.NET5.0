﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_MVVM_Base.Converters
{
    public class NullToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null || 
                (value is string && string.IsNullOrWhiteSpace((string)value)) ||
                (value is int && (int)value == 0))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}