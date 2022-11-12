using Common;
using System.Text.Json.Serialization;

#nullable enable
namespace VoiceGenerator.Clova
{
    public class Auth : BindableBase
    {
        [JsonIgnore]
        private string? _client_id;
        [JsonIgnore]
        private string? _client_secret;

        [JsonPropertyName("client_id")]
        public string? ClientId { get => _client_id; set => _client_id = value; }
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get => _client_secret; set => _client_secret = value; }
    }
}
