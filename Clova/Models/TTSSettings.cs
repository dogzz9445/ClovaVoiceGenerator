using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

#nullable enable
namespace VoiceGenerator.Clova
{
    public enum ClovaSoundFormat
    {
        wav,
        mp3,
    }

    public class TTSSettings
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
        public int? Volume { get => _volume; set => _volume = value; }
        [JsonPropertyName("speed")]
        public int? Speed { get => _speed; set => _speed = value; }
        [JsonPropertyName("pitch")]
        public int? Pitch { get => _pitch; set => _pitch = value; }
        [JsonPropertyName("format")]
        public ClovaSoundFormat? Format { get => _format; set => _format = value; }
        [JsonPropertyName("speaker_name")]
        public string? SpeakerName { get => _speakerName; set => _speakerName = value; }
    }
}
