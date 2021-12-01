using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectRecipe.Converters
{
    //Need to be tested
    public class ImageToByteArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null) return null;

                var image = (ImageSource)value;

                StreamImageSource streamImageSource = (StreamImageSource)image;
                System.Threading.CancellationToken cancellationToken =
                System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;
                byte[] bytesAvailable = new byte[stream.Length];
                stream.Read(bytesAvailable, 0, bytesAvailable.Length);
                return bytesAvailable;
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
