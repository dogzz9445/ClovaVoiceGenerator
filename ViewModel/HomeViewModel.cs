using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Clova;
using System.Windows.Media;
using System.Windows;
using System.IO;
using VoiceGenerator.Model;

namespace VoiceGenerator.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        MediaPlayer media = new MediaPlayer();

        #region Properties
        private Auth _auth;
        private ClovaVoice _clovaVoice;
        public Auth Auth { get => _auth ??= JsonHelper.ReadFile<Auth>("Resources/auth.json"); set => _auth = value; }
        public ClovaVoice ClovaVoice { get => _clovaVoice ??= new ClovaVoice(clientId: Auth.ClientId, clientSecret: Auth.ClientSecret, speaker: null); set => _clovaVoice = value; }

        private readonly ObservableCollection<ClovaSpeaker> _speakers = new ObservableCollection<ClovaSpeaker>();
        public ObservableCollection<ClovaSpeaker> Speakers { get => _speakers; }

        private readonly ObservableCollection<Sound> _sounds = new ObservableCollection<Sound>();
        public ObservableCollection<Sound> Sounds { get => _sounds; }

        private ClovaSpeaker _selectedSpeaker = null;
        public ClovaSpeaker SelectedSpeaker { get => _selectedSpeaker; set => SetProperty(ref _selectedSpeaker, value); }

        private string _conversionText;
        public string ConversionText { get => _conversionText; set => SetProperty(ref _conversionText, value); }
        #endregion

        #region Command
        private DelegateCommand _clearConversionTextCommand;
        public DelegateCommand ClearConversionTextCommand
        {
            get => _clearConversionTextCommand ??= new DelegateCommand(() =>
            {
                ConversionText = string.Empty;
            });
        }
        private DelegateCommand _convertConversionTextCommand;
        public DelegateCommand ConvertConversionTextCommand
        {
            get => _convertConversionTextCommand ??= new DelegateCommand(() =>
            {
                string generatedFilename = ClovaVoice.Generate(ConversionText, SelectedSpeaker);
                ConversionText = generatedFilename;
            });
        }

        private DelegateCommand<ClovaSpeaker> _playSample1Command;
        public DelegateCommand<ClovaSpeaker> PlaySample1Command
        {
            get => _playSample1Command ??= new DelegateCommand<ClovaSpeaker>((speaker) =>
            {
                PlaySound($"Resources/voice/샘플1/{speaker.EnglishName}.wav");
            });
        }

        private DelegateCommand<ClovaSpeaker> _playSample2Command;
        public DelegateCommand<ClovaSpeaker> PlaySample2Command
        {
            get => _playSample2Command ??= new DelegateCommand<ClovaSpeaker>((speaker) =>
            {
                PlaySound($"Resources/voice/샘플2/{speaker.EnglishName}.wav");
            });
        }
        #endregion

        public void PlaySound(string filename)
        {
            media.Stop();
            Uri soundUri = new Uri(Path.Combine(Application.Current.StartupUri.ToString(), filename));
            Console.WriteLine(soundUri.ToString());
            media.Open(new Uri(Path.Combine(Application.Current.StartupUri.ToString(), filename)));
        }

        public HomeViewModel()
        {
            // _clovaVoice.LoadConfig();
            Initialize();
        }

        private void Initialize()
        {
            // 화자 데이터 가져오기, 정해져있음
            // FIXME: 혹시 API 요청으로 리스트 가져올 수 있으면 가져와서 넣어주기
            ClovaSpeaker.Speakers.ForEach(item => Speakers.Add(item));
            SelectedSpeaker = Speakers.First();
        }
    }
}
