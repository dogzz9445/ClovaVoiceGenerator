using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common
{
    public static class JsonHelper
    {
        public static T ReadFile<T>(string filename) where T : class
        {
            if (File.Exists(filename))
            {
                var json = File.ReadAllText(filename);
                if (!string.IsNullOrEmpty(json))
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            return null;
        }

        public static async Task<T> ReadFileAsync<T>(string fullFileName)
            where T : new()
        {
            if (File.Exists(fullFileName))
            {
                using (var reader = File.OpenText(fullFileName))
                {
                    string json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            else
            {
                return default;
            }
        }

        public static async Task WriteFileAsync<T>(string fullFileName, T jsonObject)
        {
            var directoryName = Path.GetDirectoryName(fullFileName);
            if (!string.IsNullOrEmpty(directoryName))
            {
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
            }
            using (var file = File.CreateText(fullFileName))
            {
                using (var writer = new JsonTextWriter(file))
                {
                    string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented,
                        new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    await writer.WriteRawAsync(json);
                }
            }
        }

        public static async Task<T> GetJsonStreamAsync<T>(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return default;
        }
    }
}
