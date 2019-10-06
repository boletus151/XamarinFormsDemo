namespace XFDemo.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class BoolToNegationConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(value is bool && (bool)value);
        }

        #endregion
    }
}