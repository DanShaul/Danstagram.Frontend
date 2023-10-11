using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Danstagram.Services
{
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() != typeof(byte[]))
            {
                throw new ArgumentException("Wrong value type");
            }
            var ms = new MemoryStream((byte[])value);
            return ImageSource.FromStream(() => ms);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(Stream))
            {
                Console.WriteLine($"Type of: {value.GetType()}");
                throw new ArgumentException("Wrong value type");
            }
            MemoryStream ms = new MemoryStream();
            ((Stream)value).CopyTo(ms);
            byte[] array = ms.ToArray();
            ms.Dispose();
            return array;
        }
    }
}
