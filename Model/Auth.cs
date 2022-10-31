using System.Text.Json.Serialization;
using Common;

namespace VoiceGenerator.Model
{
    public class Auth : BindableBase
    {
        [JsonPropertyName("client_id")]
        private string _client_id;
        [JsonPropertyName("client_secret")]
        private string _client_secret;

        [JsonIgnore]
        public string ClientId { get => _client_id; set => _client_id = value; }
        [JsonIgnore]
        public string ClientSecret { get => _client_secret; set => _client_secret = value; }
    }
}
