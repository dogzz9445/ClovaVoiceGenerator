using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VoiceGenerator.Clova;

namespace VoiceGenerator.Model
{
    public class AppSettings : BindableBase
    {
        [JsonIgnore]
        private ClovaSettings _clovaSettings;
        [JsonPropertyName("clova_settings")]
        public ClovaSettings ClovaSettings { get => _clovaSettings; set => SetObservableProperty(ref _clovaSettings, value); }
    }
}
