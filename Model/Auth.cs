using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGenerator.Model
{
    public class Auth
    {
        [JsonProperty("client_id")]
        private string _client_id;
        [JsonProperty("client_secret")]
        private string _client_secret;

        [JsonIgnore]
        public string ClientId { get => _client_id; set => _client_id = value; }
        [JsonIgnore]
        public string ClientSecret { get => _client_secret; set => _client_secret = value; }
    }
}
