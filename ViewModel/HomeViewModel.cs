using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clova.Type;
using Common;

namespace VoiceGenerator.ViewModel
{
    public class HomeViewModel : BindableBase
    {
        private readonly ObservableCollection<string> _speakerTypes = new ObservableCollection<string>();
        public ObservableCollection<string> SpeakerTypes { get => _speakerTypes; }


        public HomeViewModel()
        {

            Initialize();
        }

        private void Initialize()
        {
            Enum.GetNames(typeof(SpeakerType)).ToList().ForEach(item => _speakerTypes.Add(item));
        }
    }
}
