using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace VoiceGenerator.ViewModel
{
    public class TestViewModel : BindableBase
    {

        #region Command

        private DelegateCommand _testCreateFileCommand;
        public DelegateCommand TestCreateFileCommand
        {
            get => _testCreateFileCommand ??= new DelegateCommand(() =>
            {
                using (Stream stream = PathHelper.OpenWriteNextAvailableIndex($"Resources/voice/test.wav"))
                {
                    stream.Flush();
                }
            });
        }

        #endregion
    }
}
