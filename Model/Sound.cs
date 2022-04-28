using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace VoiceGenerator.Model
{
    public class Sound : BindableBase
    {
        [JsonProperty("SoundType")]
        private int _soundType = 2;
        [JsonProperty("SoundName")]
        private string _soundName = "";
        [JsonProperty("LoopingUse")]
        private int _loopingUse = -1;

        [JsonProperty("SoundText")]
        private string _soundText;
        [JsonProperty("Description")]
        private string _description;
    }
}
