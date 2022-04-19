using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clova
{
    public class ClovaVoice
    {
        string client_id = null;
        string client_secret = null;
        const string URL_TTS = "https://naveropenapi.apigw.ntruss.com/tts-premium/v1/tts";

        public async void RequestAsync(string text, Speaker speaker)
        {
            if (string.IsNullOrEmpty(client_id))
            {
                throw new ArgumentNullException("Null is ", nameof(client_id));
            }
            if (string.IsNullOrEmpty(client_secret))
            {
                throw new ArgumentNullException("Null is ", nameof(client_secret));
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Null is ", nameof(text));
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_TTS);
            request.Headers.Add("X-NCP-APIGW-API-KEY-ID", client_id);
            request.Headers.Add("X-NCP-APIGW-API-KEY", client_secret);
            request.Method = "POST";
            byte[] byteDataParams = Encoding.UTF8.GetBytes($"speaker={speaker.EnglishName}&volume=0&speed=0&pitch=0&format=mp3&text={text}");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = await request.GetRequestStreamAsync();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            Console.WriteLine("status=" + status);
            using (Stream output = File.OpenWrite("c:/tts.mp3"))
            {
                using (Stream input = response.GetResponseStream())
                {
                    input.CopyTo(output);
                }
            }
            Console.WriteLine("c:/tts.mp3 was created");
        }
    }
}
