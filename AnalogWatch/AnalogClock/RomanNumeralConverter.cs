using System;
using System.Globalization;
using System.Windows.Data;

namespace AnalogWatch.AnalogClock
{
    public class RomanNumeralConverter : IValueConverter
    {
        private string[] romanNumerals = new [] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" }; 
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hour = (int)value;
            return romanNumerals[hour - 1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
