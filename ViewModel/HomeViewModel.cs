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
using System.ComponentModel;
using VoiceGenerator.Common;

namespace VoiceGenerator.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        MediaPlayer media = new MediaPlayer();

        #region Properties
        private static readonly AsyncLocker _settingsLocker = new AsyncLocker();
        private static readonly AsyncLocker _authLocker = new AsyncLocker();

        private static Func<Action, Task> callOnUiThread = async (handler) =>
            await Application.Current.Dispatcher.InvokeAsync(handler);

        private Auth _auth;
        public Auth Auth { get => _auth; set => SetObservableProperty(ref _auth, value); }

        private ClovaVoiceAPIController _clovaVoiceController;
        public ClovaVoiceAPIController ClovaVoiceController { get => _clovaVoiceController; set => SetProperty(ref _clovaVoiceController, value); }

        private AppSettings _appSettings;
        public AppSettings AppSettings { get => _appSettings; set => SetObservableProperty(ref _appSettings, value); }

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
                ConversionText = await ClovaVoiceController.GenerateAsync(ConversionText, SelectedSpeaker);
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

        public HomeViewModel()
        {
            Initialize();
        }

        private async void Initialize()
        {
            // 화자 데이터 가져오기, 정해져있음
            // FIXME: 혹시 API 요청으로 리스트 가져올 수 있으면 가져와서 넣어주기
            _appSettings = await _settingsLocker.LockAsync(
                async () => await JsonHelper.ReadFileAsync<AppSettings>("Resources/appsettings.json"));
            _appSettings.PropertyChanged += async (s, e) =>
            {
                await _settingsLocker.LockAsync(
                    async () => await JsonHelper.WriteFileAsync("Resources/appsettings.json", _appSettings));
            };
            _auth = await _authLocker.LockAsync(
                async () => await JsonHelper.ReadFileAsync<Auth>("Resources/auth.json"));
            _auth.PropertyChanged += async (s, e) =>
            {
                await _authLocker.LockAsync(
                    async () => await JsonHelper.WriteFileAsync("Resources/auth.json", _auth));
            };

            await InitializeClova();
        }

        private async Task InitializeClova()
        {
            _clovaVoiceController = new ClovaVoiceAPIController(clientId: Auth.ClientId, clientSecret: Auth.ClientSecret, settings: _appSettings.ClovaSettings);
            await _clovaVoiceController.Initialize();
            _clovaVoiceController.Speakers.ForEach(item => Speakers.Add(item));

            SelectedSpeaker = Speakers.FirstOrDefault();
        }

        public void PlaySound(string filename)
        {
            media.Stop();
            Uri soundUri = new Uri(Path.Combine(Application.Current.StartupUri.ToString(), filename));
            media.Open(new Uri(Path.Combine(Application.Current.StartupUri.ToString(), filename)));
        }
    }
}
