using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VoiceGenerator.Clova
{
    public class ClovaVoiceController : BindableBase
    {
        #region 정적 Property
        private const string URL_CLOVE_TTS = "https://naveropenapi.apigw.ntruss.com/tts-premium/v1/tts";
        private const string REQUEST_HEADER_KEY_ID = "X-NCP-APIGW-API-KEY-ID";
        private const string REQUEST_HEADER_KEY = "X-NCP-APIGW-API-KEY";
        private const string REQUEST_METHOD = "POST";
        private const string CONTENT_TYPE = "application/x-www-form-urlencoded";
        #endregion

        private string _speakerListFileName;

        private string _clientId;
        private string _clientSecret;

        private string _logMessage = "";
        public string LogMessage { get => _logMessage; set => SetProperty(ref _logMessage, value); }

        private ClovaSettings _voiceSettings;
        public ClovaSettings VoiceSettings { get => _voiceSettings; set => _voiceSettings = value; }
        
        private ClovaSpeaker _selectedSpeaker;
        public ClovaSpeaker SelectedSpeaker { get => _selectedSpeaker; set => _selectedSpeaker = value; }

        public List<ClovaSpeaker> _speakers;
        public List<ClovaSpeaker> Speakers { get => _speakers ??= new List<ClovaSpeaker>(); }

        public ClovaVoiceController(
            string clientId,
            string clientSecret,
            ClovaSettings settings = null,
            ClovaSpeaker speaker = null,
            string speakerListFileName = null)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _voiceSettings = settings ??= new ClovaSettings();
            _selectedSpeaker = speaker ??= Speakers.FirstOrDefault(item => item.KoreanName == "아라");
            _speakerListFileName = string.IsNullOrEmpty(speakerListFileName) ? "Resources/speakers.json" : speakerListFileName;
        }

        public async Task InitializeAsync()
        {
            await LoadSpeakersAsync(_speakerListFileName);
            LogMessage = "초기화 완료";
        }

        public async Task<List<ClovaSpeaker>> LoadSpeakersAsync(string fileName)
        {
            var speakers = await JsonHelper.ReadFileAsync<List<ClovaSpeaker>>(fileName);
            if (speakers != null)
            {
                Speakers.AddRange(speakers);
            }
            return Speakers;
        }

        public string ConvertKoreanToEnglish(string korean)
        {
            if (string.IsNullOrEmpty(korean))
            {
                return null;
            }
            var speaker = Speakers.FirstOrDefault(item => item.KoreanName == korean);
            if (speaker == null)
            {
                return null;
            }
            return speaker.Name;
        }

        public string ConvertEnglishToKorean(string english)
        {
            if (string.IsNullOrEmpty(english))
            {
                return null;
            }
            var speaker = Speakers.FirstOrDefault(item => item.Name == english);
            if (speaker == null)
            {
                return null;
            }
            return speaker.KoreanName;
        }

        private async Task<Stream> RequestAsync(string text)
        {
            if (string.IsNullOrEmpty(_clientId))
            {
#if DEBUG
                throw new ArgumentNullException("Null is ", nameof(_clientId));
#else
                return null;
#endif
            }
            if (string.IsNullOrEmpty(_clientSecret))
            {
#if DEBUG
                throw new ArgumentNullException("Null is ", nameof(_clientSecret));
#else
                return null;
#endif
            }
            if (string.IsNullOrEmpty(text))
            {
#if DEBUG
                throw new ArgumentNullException("Null is ", nameof(text));
#else
                return null;
#endif
            }

            byte[] byteDataParams = Encoding.UTF8.GetBytes($"speaker={SelectedSpeaker.Name}&volume={VoiceSettings.Volume}&speed={VoiceSettings.Speed}&pitch={VoiceSettings.Pitch}&format={VoiceSettings.Format}&text={text}");

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

            // TODO:
            // API 에러 내용 표시
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
            SelectedSpeaker = speaker ?? SelectedSpeaker;
            string directoryName = "Resources/voice";
            string filename = $"voice.{VoiceSettings.Format}";
            string fullFilename = Path.Combine(directoryName, filename);
            string generatedFilename = await RequestAndSaveAsync(fullFilename, text);
            return generatedFilename;
        }
    }
}
