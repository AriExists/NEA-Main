using NEA_Main.Commands;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Models;
using NEA_Main.Data;
using Microsoft.VisualBasic.ApplicationServices;

namespace NEA_Main.ViewModels
{
    public class CreateGroupChatViewModel : ViewModelBase
    {
        public ICommand NavigateBack{  get; set; }
        public ICommand CreateGroupChat { get; set; }
        private Random _rnd = new Random();
        internal MasterContext context = new MasterContext();

        private readonly AccountUser _sessionUser;

        private string _groupChatName;
        public string GroupChatName
        {
            get => _groupChatName;
            set
            {
                _groupChatName = value;
                OnProperyChanged(nameof(GroupChatName));
            }
        }

        private string? _groupChatDescription;
        public string? GroupChatDescription
        {
            get => _groupChatDescription;
            set
            {
                _groupChatDescription = value;
                OnProperyChanged(nameof(GroupChatDescription));
            }
        }

        private string? _result;
        public string? Result
        {
            get => _result;
            set
            {
                _result = value;
                OnProperyChanged(nameof(Result));
            }
        }

        private System.Windows.Media.Brush _responseColour;
        public System.Windows.Media.Brush ResponseColour
        {
            get => _responseColour;
            set
            {
                _responseColour = value;
                OnProperyChanged(nameof(ResponseColour));
            }
        }

        public CreateGroupChatViewModel(NavStore navStore, AccountUser sessionUser)
        {
            NavigateBack = new NavigateMainApp(navStore, sessionUser);
            CreateGroupChat = new CreateGroupChatCommand(this);
            _sessionUser = sessionUser;
            ResponseColour = System.Windows.Media.Brushes.Red;
        }




        private int joinId;
        private bool validID;
        public void TryCreateGroupChat()
        {
            if (!string.IsNullOrEmpty(GroupChatName))
            {
                validID = false;
                while (!validID)
                {
                    validID = true;
                    joinId = _rnd.Next(1000, 10000);
                    foreach (GroupChat gc in context.GroupChats)
                    {
                        if (joinId == gc.JoinId) // makes sure generated ID to join the server is unique
                        {
                            validID = false;
                        }
                    }
                }



                GroupChat newGroupChat = new GroupChat()
                {
                    JoinId = joinId,
                    Name = GroupChatName,
                    Description = GroupChatDescription,
                };




                context.Add(newGroupChat);
                context.SaveChanges();

                ChatThread newChatThread = new ChatThread()
                {
                    Description = "The default general chat",
                    Name = "General",
                    GroupChatId = newGroupChat.Id

                };

                context.Add(newChatThread);
                context.SaveChanges();

                ResponseColour = System.Windows.Media.Brushes.Green;
                Result = "Groupchat created successfully!";
            }
            else
            {
                Result = "Groupchat name must entered";
            }
        }
    }
}
