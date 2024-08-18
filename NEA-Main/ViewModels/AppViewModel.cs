using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Models;
using NEA_Main.Views;
using NEA_Main.Data;
using System.ComponentModel;
using NEA_Main.Commands;
using System.Windows;

namespace NEA_Main.ViewModels
{
    class AppViewModel : ViewModelBase
    {
        public AccountUser SessionUser { get; set; }
        private GroupChat? _currentGroupChat;
        public ICommand NavigateCreateGroupchatCommand { get; set; }
        public ICommand JoinGCPopupCommand { get; set; }
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

        private Window? _openModal;
        public Window? OpenModal
        {
            get => _openModal;
            set
            {
                _openModal = value;
                OnProperyChanged(nameof(OpenModal));
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
            NavigateCreateGroupchatCommand = new NavigateCreateGroupChat(navStore, sessionUser);
            JoinGCPopupCommand = new OpenJoinGroupChatModalCommand(this);
            
        }

        public void OpenChangeGroupChatModal()
        {
            OpenModal = new JoinGroupChatModal();
            OpenModal.ShowDialog();
        }

        MasterContext context = new MasterContext();
        private int _groupChatJoincode;
        public void JoinGroupChat(string code)
        {
            _groupChatJoincode = Convert.ToInt32(_groupChatJoincode);
            if (10000 <= _groupChatJoincode || _groupChatJoincode < 0) {
                return;
            }

            foreach (GroupChat chat in context.GroupChats)
            {
                if (chat.JoinId == _groupChatJoincode)
                {
                    foreach(AccountUser user in chat.Users)
                    {
                        if (user.Id == SessionUser.Id)
                        {
                            return;
                        }
                    }

                    // add id to groupchat user list and vise versa

                }
            }

        }

    }
}
