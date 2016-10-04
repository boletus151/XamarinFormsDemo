namespace XamarinFormsDemo.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class HexadecimalToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            var colorString = str?.Replace("#", "");
            if(string.IsNullOrEmpty(colorString))
            {
                return Color.Black;
            }

            var color = Color.FromHex(colorString);
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Color tu Hex not supported");
        }
    }
}