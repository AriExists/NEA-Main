using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.ViewModels
{
    public class JoinGroupChatViewModel : ViewModelBase
    {
        private string? _result;
        public string? Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnProperyChanged(nameof(Result));
            }
        }

        private System.Windows.Media.Brush _responseColour;
        public System.Windows.Media.Brush ResponseColour
        {
            get { return _responseColour; }
            set
            {
                _responseColour = value;
                OnProperyChanged(nameof(ResponseColour));
            }
        }
        public JoinGroupChatViewModel()
        {
            _responseColour = System.Windows.Media.Brushes.Red;
            ResponseColour = _responseColour;
        }
    }
}
