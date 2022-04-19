using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Clova;

namespace VoiceGenerator.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        #region Properties
        private ClovaVoice _clovaVoice = null;

        private readonly ObservableCollection<Speaker> _speakers = new ObservableCollection<Speaker>();
        public ObservableCollection<Speaker> Speakers { get => _speakers; }

        private Speaker _selectedSpeaker = null;
        public Speaker SelectedSpeaker { get => _selectedSpeaker; set => SetProperty(ref _selectedSpeaker, value); }

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
                _clovaVoice.RequestAsync(ConversionText, SelectedSpeaker);
            });
        }
        #endregion

        public HomeViewModel()
        {
            _clovaVoice = new ClovaVoice();
            // _clovaVoice.LoadConfig();
            Initialize();
        }

        private void Initialize()
        {
            // 화자 데이터 가져오기, 정해져있음
            // FIXME: 혹시 API 요청으로 리스트 가져올 수 있으면 가져와서 넣어주기
            Speaker.Speakers.ForEach(item => Speakers.Add(item));
            SelectedSpeaker = Speakers.First();
        }
    }
}
