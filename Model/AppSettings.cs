using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VoiceGenerator.Clova;

#nullable enable
namespace VoiceGenerator.Model
{
    public class AppSettings : BindableBase
    {
        [JsonIgnore]
        private string? _speakerListFileName;
        [JsonPropertyName("speaker_list_filename")]
        public string? SpeakerListFileName { get => _speakerListFileName; set => SetProperty(ref _speakerListFileName, value); }

        [JsonIgnore]
        private ClovaSettings? _voiceSettings;
        [JsonPropertyName("clova_settings")]
        public ClovaSettings? VoiceSettings { get => _voiceSettings; set => SetObservableProperty(ref _voiceSettings, value); }

        public AppSettings() : this(null) {}

        public AppSettings(
            string? speakerListFileName = null,
            ClovaSettings? voiceSettings = null
            )
        {
            _speakerListFileName = speakerListFileName ?? "";
            _voiceSettings = voiceSettings ?? new ClovaSettings();
        }
    }
}
