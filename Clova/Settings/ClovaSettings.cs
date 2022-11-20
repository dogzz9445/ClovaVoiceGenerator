using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using Common;

#nullable enable
namespace VoiceGenerator.Clova
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ClovaSoundFormat
    {
        wav,
        mp3,
    }

    public class ClovaSettings : BindableBase
    {
        [JsonIgnore]
        private int? _volume;
        [JsonIgnore]
        private int? _speed;
        [JsonIgnore]
        private int? _pitch;
        [JsonIgnore]
        private ClovaSoundFormat? _format;
        [JsonIgnore]
        private string? _speakerName;

        [JsonPropertyName("volume")]
        public int? Volume { get => _volume; set => SetProperty(ref _volume, value); }
        [JsonPropertyName("speed")]
        public int? Speed { get => _speed; set => SetProperty(ref _speed, value); }
        [JsonPropertyName("pitch")]
        public int? Pitch { get => _pitch; set => SetProperty(ref _pitch, value); }
        [JsonPropertyName("format")]
        public ClovaSoundFormat? Format { get => _format; set => SetProperty(ref _format, value); }
        [JsonPropertyName("speaker_name")]
        public string? SpeakerName { get => _speakerName; set => SetProperty(ref _speakerName, value); }
    }
}
