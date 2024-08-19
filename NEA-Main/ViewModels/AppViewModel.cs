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
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.RightsManagement;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using NEA_Main.Models.Generated;

namespace NEA_Main.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        public AccountUser SessionUser { get; set; }
        private GroupChat? _currentGroupChat;
        public ICommand NavigateCreateGroupchatCommand { get; set; }
        public ICommand JoinGCPopupCommand { get; set; }
        public ICommand SendMessageCommand {  get; set; }
        public ICommand OpenCreateThreadCommand { get; set; }

        private DispatcherTimer _timer;

        public GroupChat? CurrentGroupChat
        {
            get => _currentGroupChat;
            set
            {
                _currentGroupChat = value;
                OnProperyChanged(nameof(CurrentGroupChat));
                UpdateCurrentGroupChatThreads();
                Messages.Clear();
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

        private ObservableCollection<GroupChat> _joinedGroupChats;
        public ObservableCollection<GroupChat> JoinedGroupChats
        {
            get => _joinedGroupChats;
            set
            {
                _joinedGroupChats = value;
                OnProperyChanged(nameof(JoinedGroupChats));
            }
        }

        private ObservableCollection<ChatThread> _currentChatThreads;
        public ObservableCollection<ChatThread> CurrentChatThreads
        {
            get => _currentChatThreads;
            set
            {
                _currentChatThreads = value;
                OnProperyChanged(nameof(CurrentChatThreads));
            }
        }

        private ChatThread _currentThread;
        public ChatThread CurrentThread
        {
            get => _currentThread;
            set
            {
                _currentThread = value;
                OnProperyChanged(nameof(CurrentThread));
                Messages.Clear();
            }
        }

        private ObservableCollection<Message> _messages;
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnProperyChanged(nameof(Messages));
    
            }
        }

        private string _inputMessage;
        public string InputMessage
        {
            get => _inputMessage;
            set
            {
                _inputMessage = value;
                OnProperyChanged(nameof(InputMessage));
            }
        }

        public AppViewModel(NavStore navStore, AccountUser sessionUser)
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(Update_Tick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();

            

            SessionUser = sessionUser;
            NavigateCreateGroupchatCommand = new NavigateCreateGroupChat(navStore, sessionUser);
            JoinGCPopupCommand = new OpenJoinGroupChatModalCommand(this);
            SendMessageCommand = new SendMessageCommand(this);
            OpenCreateThreadCommand = new OpenCreateThreadCommand(this);
            UpdateViewGroupChatData();
            UpdateCurrentGroupChatThreads();
            Messages = new ObservableCollection<Message>();
            CurrentChatThreads = new ObservableCollection<ChatThread>();
            updateGroupChatSelector();


        }

        public void OpenNewModal(Window modal, ViewModelBase viewModel)
        {
            if (OpenModal != null)
            {
                OpenModal.Close();
                OpenModal = null;
            }

            //OpenModalViewModel = new JoinGroupChatViewModel(this);
            //OpenModal = new JoinGroupChatModal();
            OpenModalViewModel = viewModel;
            OpenModal = modal;
            OpenModal.DataContext = OpenModalViewModel;
            OpenModal.ShowDialog();
            
        }

        public void updateGroupChatSelector()
        {
            var joinedGCs = new ObservableCollection<GroupChat>();
            foreach (GroupChat chat in JoinedGroupChats) 
            {
                joinedGCs.Add(chat);
            }
            JoinedGroupChats = joinedGCs;

            if (CurrentGroupChat != null) 
            {
                var currentGcThreads = new ObservableCollection<ChatThread>();
                foreach (ChatThread t in CurrentChatThreads)
                {
                    currentGcThreads.Add(t);
                }

                CurrentChatThreads = currentGcThreads;
            }
        }

        private async void Update_Tick(object sender, EventArgs e)
        {
            await UpdateChat();
        }

        public async Task UpdateChat()
        {

            
            await Task.Run(() =>
            {
                updateGroupChatSelector();
                if (CurrentThread != null)
                {
                    var messages = new ObservableCollection<Message>();
                    foreach (Message msg in context.Messages)
                    {
                        if (msg.ChatThreadId == CurrentThread.Id)
                        {
                            messages.Add(msg);
                        }
                    }
                    Messages = messages;
                }
            });
        }

        public void SendMessage()
        {
            if (!string.IsNullOrEmpty(InputMessage) && CurrentThread != null)
            {
                Message newMessage = new Message()
                { 
                    Text = InputMessage,
                    TimeSent = TimeOnly.FromDateTime(DateTime.Now),
                    SenderId = SessionUser.Id,
                    SenderUsername = SessionUser.Username,
                    ChatThreadId = CurrentThread.Id,
                    DateSent = DateTime.Now
                    
                };
                context.Add(newMessage);
                context.SaveChanges();

                InputMessage = "";
            }
        }


        private List<int> _joinedChatIds = new List<int>();
        public void UpdateViewGroupChatData() // notifies the view of all the groupchats that the user is in
        {
            JoinedGroupChats = new ObservableCollection<GroupChat>();
            var userRelations = context.AccountUserGroupChats
                                .Where(agc => agc.AccountUserId == SessionUser.Id);
            
                                
            foreach (AccountUserGroupChat relation in userRelations)
            {
                _joinedChatIds.Add(relation.GroupChatId);
            }

            foreach (int id in _joinedChatIds)
            {
                foreach (GroupChat chat in context.GroupChats)
                {
                    if (chat.Id == id)
                    {
                        JoinedGroupChats.Add(chat);
                    }
                }
            }
        }

        public void UpdateCurrentGroupChatThreads()
        {
            CurrentChatThreads = new ObservableCollection<ChatThread>();
            if (CurrentGroupChat != null)
            {
                foreach (ChatThread thread in context.ChatThreads)
                {
                    if (thread.GroupChatId == CurrentGroupChat.Id)
                    {  
                        CurrentChatThreads.Add(thread);
                    }
                }
            }
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
