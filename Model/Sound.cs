using System.Text.Json.Serialization;
using Common;

namespace VoiceGenerator.Model
{
    public class Sound : BindableBase
    {
        [JsonPropertyName("SoundType")]
        private int _soundType = 2;
        [JsonPropertyName("SoundName")]
        private string _soundName = "";
        [JsonPropertyName("LoopingUse")]
        private int _loopingUse = -1;

        [JsonPropertyName("SoundText")]
        private string _soundText;
        [JsonPropertyName("Description")]
        private string _description;
    }
}
