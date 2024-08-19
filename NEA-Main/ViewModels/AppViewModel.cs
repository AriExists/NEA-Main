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
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

namespace NEA_Main.ViewModels
{
    public class AppViewModel : ViewModelBase
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

        private ViewModelBase _openModalViewModel;
        public ViewModelBase OpenModalViewModel
        {
            get => _openModalViewModel;
            set
            {
                _openModalViewModel = value;
                OnProperyChanged(nameof(OpenModalViewModel));
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
            OpenModalViewModel = new JoinGroupChatViewModel(this);
            OpenModal = new JoinGroupChatModal();
            OpenModal.DataContext = OpenModalViewModel;
            OpenModal.ShowDialog();
        }

        MasterContext context = new MasterContext();
        private int _groupChatJoincode;
        private OutputResult _out;
        private GroupChat _targetGroupchat;
        public OutputResult JoinGroupChat(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                _out = new OutputResult("No ID input", System.Windows.Media.Brushes.Red);
                return _out;
            }
 
            if (code.Length > 4)
            {
                _out = new OutputResult("Invalid ID", System.Windows.Media.Brushes.Red);
                return _out;
            }
            _groupChatJoincode = Convert.ToInt32(code);

            foreach (GroupChat chat in context.GroupChats)
            {
                if (chat.JoinId == _groupChatJoincode)
                {
                    _targetGroupchat = chat;
                }
            }

            if (_targetGroupchat != null)
            {
                foreach (AccountUserGroupChat r in context.AccountUserGroupChats)
                {
                    if (r.GroupChatId == _targetGroupchat.Id)
                    {
                        if (r.AccountUserId == SessionUser.Id)
                        {
                            _out = new OutputResult("Users is already in groupchat", System.Windows.Media.Brushes.Red);
                            return _out;
                        }
                    }
                }

                // adds id to groupchat user list and vise versa
                AccountUserGroupChat relation = new AccountUserGroupChat()
                {
                    AccountUserId = SessionUser.Id,
                    GroupChatId = _targetGroupchat.Id,
                };

                context.AccountUserGroupChats.Add(relation);
                context.SaveChanges();

                _out = new OutputResult("Groupchat Joined successfully", System.Windows.Media.Brushes.Green);
                return _out;
            }

            _out = new OutputResult("Groupchat not found", System.Windows.Media.Brushes.Red);
            return _out;
        }

    }
}
