using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceGenerator.Clova;

namespace VoiceGenerator.Model
{
    public class AppSettings
    {
        private ClovaSettings _clovaSettings;
        public ClovaSettings ClovaSettings { get => _clovaSettings; set => _clovaSettings = value; }
    }
}
