using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Common;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable
namespace VoiceGenerator.Clova
{
    // Json 파일로 설정 값 빼기
    public class ClovaSpeaker : BindableBase
    {
        [JsonIgnore]
        private string? _name;
        [JsonPropertyName("name")]
        public string? Name { get => _name; set => SetProperty(ref _name, value); }
        [JsonIgnore]
        private string? _koreanName;
        [JsonPropertyName("korean_name")]
        public string? KoreanName { get => _koreanName; set => SetProperty(ref _koreanName, value); }
        [JsonIgnore]
        private string? _language;
        [JsonPropertyName("language")]
        public string? Language { get => Language; set => SetProperty(ref _language, value); }
        [JsonIgnore]
        private string? _description;
        [JsonPropertyName("description")]
        public string? Description { get => _description; set => SetProperty(ref _description, value); }

        public ClovaSpeaker(string? name, string? koreanName, string? description)
        {
            Name = name;
            KoreanName = koreanName;
            Description = description;
        }

        public static async Task LoadSpeakers(string fileName)
        {
            var speakers = await JsonHelper.ReadFileAsync<List<ClovaSpeaker>>(fileName);
            if (speakers == null)
            {
                return;
            }
            Speakers.AddRange(speakers);
        }

        public static List<ClovaSpeaker>? _speakers;
        public static List<ClovaSpeaker> Speakers { get => _speakers ??= new List<ClovaSpeaker>(); }

        public static string? ConvertKoreanToEnglish(string korean)
        {
            var speaker = Speakers.FirstOrDefault(item => item.KoreanName == korean);
            if (speaker != null)
            {
                return speaker.Name;
            }
            return null;
        }

        public static string? ConvertEnglishToKorean(string english)
        {
            var speaker = Speakers.FirstOrDefault(item => item.Name == english);
            if (speaker != null)
            {
                return speaker.KoreanName;
            }
            return null;
        }
    }

}
