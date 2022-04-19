using System;
using System.Linq;
using System.Globalization;
using System.Windows.Data;

namespace Clova
{
    [ValueConversion(typeof(Speaker), typeof(string))]
    public class SpeakerToStringValueConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            Speaker speaker = (Speaker)value;
            return speaker.KoreanName;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            return Speaker.Speakers.FirstOrDefault(item => item.KoreanName == name);
        }
    }
}
