using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Models;
using NEA_Main.Data;
using System.ComponentModel;

namespace NEA_Main.ViewModels
{
    class AppViewModel : ViewModelBase
    {
        public AccountUser SessionUser { get; set; }
        private GroupChat? _currentGroupChat;
        public GroupChat? CurrentGroupChat
        {
            get => _currentGroupChat;
            set
            {
                _currentGroupChat = value;
                OnProperyChanged(nameof(CurrentGroupChat));
            }
        }

        private string? _currentGroupChatLabel;
        public string? CurrentGroupChatLabel
        {
            get => _currentGroupChatLabel;
            set
            {
                _currentGroupChatLabel = "Current Groupchat: " + value;
                OnProperyChanged(nameof(CurrentGroupChatLabel));
            }
        }

        private string? _currentChatThreadLabel;
        public string? CurrentChatThreadLabel
        {
            get => _currentChatThreadLabel;
            set
            {
                _currentChatThreadLabel = "Current thread: " + value;
                OnProperyChanged(nameof(CurrentChatThreadLabel));
            }
        }

        public AppViewModel(NavStore navStore, AccountUser sessionUser)
        {
            SessionUser = sessionUser;
            CurrentGroupChatLabel = "None";
            CurrentChatThreadLabel = "None";
            
        }
    }
}
