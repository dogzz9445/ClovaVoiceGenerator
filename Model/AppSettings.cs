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
        private TTSSettings _ttsSettings;
        public TTSSettings TTSSettings { get => _ttsSettings; set => _ttsSettings = value; }
    }
}
