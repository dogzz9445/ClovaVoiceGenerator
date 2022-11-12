using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Data;
using System.Threading.Tasks;
using System.Globalization;

namespace VoiceGenerator.Clova
{
    [ValueConversion(typeof(ClovaSpeaker), typeof(string))]
    public class SpeakerToStringValueConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            ClovaSpeaker speaker = (ClovaSpeaker)value;
            return speaker.KoreanName;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            //string name = (string)value;
            //return ClovaVoiceAPIController.Speakers.FirstOrDefault(item => item.KoreanName == name);
            return null;
        }
    }

}
