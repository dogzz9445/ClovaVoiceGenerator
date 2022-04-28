using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clova
{
    public enum ClovaSoundFormat
    {
        mp3,
        wav
    }

    public class ClovaVoice
    {
        #region 정적 Property
        private const string URL_TTS = "https://naveropenapi.apigw.ntruss.com/tts-premium/v1/tts";
        private const string REQUEST_HEADER_KEY_ID = "X-NCP-APIGW-API-KEY-ID";
        private const string REQUEST_HEADER_KEY = "X-NCP-APIGW-API-KEY";
        private const string REQUEST_METHOD = "POST";
        private const string CONTENT_TYPE = "application/x-www-form-urlencoded";
        #endregion

        private string _clientId;
        private string _clientSecret;
        private int _volume;
        private int _speed;
        private int _pitch;
        private ClovaSoundFormat _format;
        private ClovaSpeaker _speaker;

        public ClovaSoundFormat Format { get => _format; set => _format = value; }
        public ClovaSpeaker Speaker { get => _speaker ??= ClovaSpeaker.Speakers.FirstOrDefault(item => item.KoreanName == "아라"); set => _speaker = value; }

        public ClovaVoice(
            string clientId,
            string clientSecret,
            int? volume = null,
            int? speed = null,
            int? pitch = null,
            ClovaSoundFormat? format = null,
            ClovaSpeaker speaker = null)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _volume = volume ?? 0;
            _speed = speed ?? 0;
            _pitch = pitch ?? 0;
            Format = format ?? ClovaSoundFormat.wav;
            Speaker = speaker ?? Speaker;
        }

        private async Task<Stream> RequestAsync(string text)
        {
            if (string.IsNullOrEmpty(_clientId))
            {
                throw new ArgumentNullException("Null is ", nameof(_clientId));
            }
            if (string.IsNullOrEmpty(_clientSecret))
            {
                throw new ArgumentNullException("Null is ", nameof(_clientSecret));
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("Null is ", nameof(text));
            }

            byte[] byteDataParams = Encoding.UTF8.GetBytes($"speaker={Speaker.EnglishName}&volume={_volume}&speed={_speed}&pitch={_pitch}&format={Format}&text={text}");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_TTS);
            request.Method = REQUEST_METHOD;
            request.Headers.Add(REQUEST_HEADER_KEY_ID, _clientId);
            request.Headers.Add(REQUEST_HEADER_KEY, _clientSecret);
            request.ContentType = CONTENT_TYPE;
            request.ContentLength = byteDataParams.Length;

            Stream requestStream = await request.GetRequestStreamAsync();
            requestStream.Write(byteDataParams, 0, byteDataParams.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode is not >= (HttpStatusCode)200 and < (HttpStatusCode)300)
            {
                throw new WebException($"Web request response exception: {response.StatusDescription}");
            }
            return response.GetResponseStream();
        }

        private Stream Request(string text)
        {
            Task<Stream> stream = RequestAsync(text);
            return stream.Result;
        }

        private async Task<string> RequestAndSaveAsync(string filename, string text)
        {
            var generatedFilename = PathHelper.NextAvailableIndexFilename(filename);
            using (Stream output = PathHelper.OpenWriteOverride(generatedFilename))
            {
                using (Stream input = await RequestAsync(text))
                {
                    input.CopyTo(output);
                }
            }
            return generatedFilename;
        }

        private string RequestAndSave(string filename, string text)
        {
            Task<string> taskRequestAndSave = RequestAndSaveAsync(filename, text);
            return taskRequestAndSave.Result;
        }

        public string Generate(string text, ClovaSpeaker speaker = null)
        {
            Speaker = speaker ?? Speaker;
            string directoryName = "Resources/voice";
            string filename = $"voice.{Format}";
            string fullFilename = Path.Combine(directoryName, filename);
            string generatedFilename = RequestAndSave(fullFilename, text);
            return generatedFilename;
        }
    }
}
