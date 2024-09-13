using Microsoft.VisualBasic.ApplicationServices;
using NEA_Main.Models.Generated;
using NEA_Main.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Commands;
using System.Windows.Forms;
using NEA_Main.Views;



namespace NEA_Main.ViewModels
{

    public class UserListViewModel : ViewModelBase
    {

        public ICommand RefreshUserList { get; set; }
        private readonly AppViewModel _parentViewModel;
        private List<string> _currentUsers;
        public List<string> CurrentUsers
        {
            get => _currentUsers;
            set
            {
                _currentUsers = value;
                OnProperyChanged(nameof(CurrentUsers));
            }
        }
        MasterContext context = new MasterContext();
        public UserListViewModel(AppViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
            RefreshUserList = new RefreshUserListCommand(_parentViewModel, this);
            CurrentUsers = new List<string>();
            
        }

        public void RefreshUsers()
        {
            List<int> AddIds = new List<int>();
            if (_parentViewModel.CurrentGroupChat != null)
            {
                foreach (AccountUserGroupChat augc in context.AccountUserGroupChats)
                {
                    if (augc.GroupChatId == _parentViewModel.CurrentGroupChat.Id)
                    {
                        AddIds.Add(augc.AccountUserId);
                    }
                }
            }


            if (_parentViewModel.CurrentGroupChat != null)
            {
                List<string> NewUserList = new List<string>();
                foreach (AccountUser u in context.AccountUsers)
                {
                    for (int i = 0; i < AddIds.Count; i++)
                    {
                        if (u.Id == AddIds[i])
                        {
                            NewUserList.Add(u.Username);
                            break;
                        }
                    }
                }
                CurrentUsers = NewUserList;
            }
        }
    }
}
