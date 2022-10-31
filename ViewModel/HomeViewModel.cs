using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using VoiceGenerator.Clova;
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
        public Auth Auth { get => _auth; set => SetProperty(ref _auth, value); }
        private ClovaVoice _clovaVoice;
        public ClovaVoice ClovaVoice 
        { 
            get => _clovaVoice ??= new ClovaVoice(clientId: Auth.ClientId, clientSecret: Auth.ClientSecret, speaker: null); 
            set => _clovaVoice = value;
        }

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
            get => _convertConversionTextCommand ??= new DelegateCommand(async () =>
            {
                ConversionText = await ClovaVoice.GenerateAsync(ConversionText, SelectedSpeaker);
            });
        }

        private DelegateCommand<ClovaSpeaker> _playSample1Command;
        public DelegateCommand<ClovaSpeaker> PlaySample1Command
        {
            get => _playSample1Command ??= new DelegateCommand<ClovaSpeaker>((speaker) =>
            {
                PlaySound($"Resources/voice/샘플_01/{speaker.Name}.wav");
            });
        }

        private DelegateCommand<ClovaSpeaker> _playSample2Command;
        public DelegateCommand<ClovaSpeaker> PlaySample2Command
        {
            get => _playSample2Command ??= new DelegateCommand<ClovaSpeaker>((speaker) =>
            {
                PlaySound($"Resources/voice/샘플_02/{speaker.Name}.wav");
            });
        }
        #endregion

        public void PlaySound(string filename)
        {
            media.Stop();
            Uri soundUri = new Uri(Path.Combine(Application.Current.StartupUri.ToString(), filename));
            media.Open(new Uri(Path.Combine(Application.Current.StartupUri.ToString(), filename)));
        }

        public HomeViewModel()
        {
            // _clovaVoice.LoadConfig();
            Initialize();
        }

        private async void Initialize()
        {
            // 화자 데이터 가져오기, 정해져있음
            // FIXME: 혹시 API 요청으로 리스트 가져올 수 있으면 가져와서 넣어주기
            _auth = await JsonHelper.ReadFileAsync<Auth>("Resources/auth.json");
            await ClovaSpeaker.LoadSpeakers("Resources/speakers.json");
            ClovaSpeaker.Speakers.ForEach(item => Speakers.Add(item));
            SelectedSpeaker = Speakers.FirstOrDefault();
        }
    }
}
