﻿using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGenerator.Clova
{
    public class ClovaVoice
    {
        #region 정적 Property
        private const string URL_CLOVE_TTS = "https://naveropenapi.apigw.ntruss.com/tts-premium/v1/tts";
        private const string REQUEST_HEADER_KEY_ID = "X-NCP-APIGW-API-KEY-ID";
        private const string REQUEST_HEADER_KEY = "X-NCP-APIGW-API-KEY";
        private const string REQUEST_METHOD = "POST";
        private const string CONTENT_TYPE = "application/x-www-form-urlencoded";
        #endregion

        private string _clientId;
        private string _clientSecret;
        private ClovaSettings _settings;
        private ClovaSpeaker _speaker;

        public ClovaSettings Settings { get => _settings; set => _settings = value; }
        public ClovaSpeaker Speaker { get => _speaker; set => _speaker = value; }

        public ClovaVoice(
            string clientId,
            string clientSecret,
            ClovaSettings settings = null,
            ClovaSpeaker speaker = null)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _settings = settings ??= new ClovaSettings();
            _speaker = speaker ??= ClovaSpeaker.Speakers.FirstOrDefault(item => item.KoreanName == "아라");

            Initialize();
        }

        private void Initialize()
        {

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

            byte[] byteDataParams = Encoding.UTF8.GetBytes($"speaker={Speaker.Name}&volume={Settings.Volume}&speed={Settings.Speed}&pitch={Settings.Pitch}&format={Settings.Format}&text={text}");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_CLOVE_TTS);
            request.Method = REQUEST_METHOD;
            request.Headers.Add(REQUEST_HEADER_KEY_ID, _clientId);
            request.Headers.Add(REQUEST_HEADER_KEY, _clientSecret);
            request.ContentType = CONTENT_TYPE;
            request.ContentLength = byteDataParams.Length;

            try
            {
                Stream requestStream = await request.GetRequestStreamAsync();
                requestStream.Write(byteDataParams, 0, byteDataParams.Length);
                requestStream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode is not >= (HttpStatusCode)200 and < (HttpStatusCode)300)
            {
                throw new WebException($"Web request response exception: {response.StatusDescription}");
            }
            return response.GetResponseStream();
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

        public async Task<string> GenerateAsync(string text, ClovaSpeaker speaker = null)
        {
            Speaker = speaker ?? Speaker;
            string directoryName = "Resources/voice";
            string filename = $"voice.{Settings.Format}";
            string fullFilename = Path.Combine(directoryName, filename);
            string generatedFilename = await RequestAndSaveAsync(fullFilename, text);
            return generatedFilename;
        }
    }
}
