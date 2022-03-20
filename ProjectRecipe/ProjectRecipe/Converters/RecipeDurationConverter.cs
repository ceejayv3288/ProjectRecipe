using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.Converters
{
    public class RecipeDurationConverter : IValueConverter
    {
        public static int MinutesInHour { get; set; } = 60;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null) return null;

                if (value is string durationString)
                {
                    decimal durationInMinutes = int.Parse(durationString);
                    if (durationInMinutes < MinutesInHour)
                        return $"{durationInMinutes}MIN";
                    else
                        return $"{Decimal.Divide(durationInMinutes, MinutesInHour)}HR";
                }
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
