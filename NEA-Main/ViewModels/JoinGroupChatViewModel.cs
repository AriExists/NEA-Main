using NEA_Main.Commands;
using NEA_Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Main.ViewModels
{
    public class JoinGroupChatViewModel : ViewModelBase
    {
        public ICommand JoinGroupChat { get; set; }

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

        private string? _inputJoinCode;
        public string? InputJoinCode
        {
            get => _inputJoinCode;
            set
            {
                _inputJoinCode = value;
                OnProperyChanged(nameof(InputJoinCode));
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
        public JoinGroupChatViewModel(AppViewModel parentVM)
        {
            _responseColour = System.Windows.Media.Brushes.Red;
            ResponseColour = _responseColour;
            JoinGroupChat = new JoinGroupChatCommand(parentVM, this);
        }

        public void Output(OutputResult result)
        {
            ResponseColour = result.Colour;
            Result = result.Message;
        }

    }
}
