using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProjectRecipe.Converters
{
    public class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null) return null;

                var byteArray = (byte[])value;

                var imgsrc = ImageSource.FromStream(() =>
                {
                    var ms = new MemoryStream(byteArray);
                    ms.Position = 0;
                    return ms;
                });
                return imgsrc;
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
