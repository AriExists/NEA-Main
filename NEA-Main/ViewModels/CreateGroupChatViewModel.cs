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

namespace NEA_Main.ViewModels
{
    public class CreateGroupChatViewModel : ViewModelBase
    {
        public ICommand NavigateStart{  get; set; }
        public ICommand CreateGroupChat { get; set; }
        private Random _rnd = new Random();
        internal MasterContext context = new MasterContext();

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
        
        public CreateGroupChatViewModel(NavStore navStore)
        {
            NavigateStart = new NavigateStartCommand(navStore);
            CreateGroupChat = new CreateGroupChatCommand();
        }
        private int id;
        private bool validID;
        public void TryCreateGroupChat()
        {
            if (!string.IsNullOrEmpty(GroupChatName))
            {
                //validID = false;
                //while (!validID)
                //{
                //    validID = true;
                //    id = _rnd.Next(1000, 10000);
                //    foreach (GroupChat gc in context.GroupChats)
                //    {
                //        if (id == gc.JoinId) // makes sure generated ID to join the server is unique
                //        {
                //            validID = false;
                //        }
                //    }
                //}

                //GroupChat newGroupChat = new GroupChat()
                //{
                //    JoinId = id,
                //    Name = GroupChatName,
                //    Description = GroupChatDescription,
                //    // append a general thread to the chatThreads property
                //};
            }
            else
            {
                Result = "Groupchat name must entered";
            }
        }
    }
}
