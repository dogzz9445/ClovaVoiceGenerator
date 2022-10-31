using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Common
{
    public static class JsonHelper
    {
        public static async Task<T> ReadFileAsync<T>(string fullFileName)
            where T : new()
        {
            if (!File.Exists(fullFileName))
            {
                return default;
            }
            using FileStream stream = File.OpenRead(fullFileName);
            var deserializedObject = await JsonSerializer.DeserializeAsync<T>(stream);
            await stream.DisposeAsync();
            return deserializedObject;
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
            using FileStream createStream = File.Create(fullFileName);
            await JsonSerializer.SerializeAsync(
                createStream,
                jsonObject,
                options: new JsonSerializerOptions()
                {
                    WriteIndented = true,
                });
            await createStream.DisposeAsync();
        }

#if NEWTON_SOFT_JSON
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
#endif
    }
}
