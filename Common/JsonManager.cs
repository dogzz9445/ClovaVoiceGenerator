using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class JsonManager
    {
        public static async Task<T> ReadJsonFile<T>(string fullFilePath)
            where T : new()
        {
            if (File.Exists(fullFilePath))
            {
                using (var reader = File.OpenText(fullFilePath))
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

        public static async Task WritJsonFile<T>(string fullFilePath, T jsonObject)
        {
            using (var file = File.CreateText(fullFilePath))
            {
                using (var writer = new JsonTextWriter(file))
                {
                    string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    await writer.WriteRawAsync(json);
                }
            }
        }

        public static async Task<T> GetJsonStream<T>(string url)
        {
            T jsonObject = default(T);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return jsonObject;
        }
    }
}
