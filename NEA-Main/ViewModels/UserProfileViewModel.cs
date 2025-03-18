using NEA_Main.Commands;
using NEA_Main.Data;
using NEA_Main.Models.Generated;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Main.ViewModels
{
    public class UserProfileViewModel : ViewModelBase
    {
        // property setup
        private readonly NavStore _navStore;
        private readonly AccountUser _sessionUser;

        public ICommand NavigateMainAppCommand { get; set; }
        public ICommand OpenEditProfilePopup { get; set; }

        private string _profilePictureUrl;
        public string ProfilePictureUrl
        {
            get => _profilePictureUrl;
            set
            {
                _profilePictureUrl = value;
                OnProperyChanged(nameof(ProfilePictureUrl));
            }
        }

        private string _accountUsername;
        public string AccountUsername
        {
            get { return _accountUsername; }
            set 
            {
                _accountUsername = value;
                OnProperyChanged(nameof(AccountUsername));
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

        private AccountUser _sesssionUser;

        public AccountUser SessionUser
        {
            get { return _sesssionUser; }
            set 
            {
                _sesssionUser = value;
              
            }
        }

// closes existing popup, stores new popup from passed in objects
        public void OpenNewModal(Window modal, ViewModelBase viewModel)
        {
            if (OpenModal != null)
            {
                OpenModal.Close();
                OpenModal = null;
            }

            OpenModalViewModel = viewModel;
            OpenModal = modal;
            OpenModal.DataContext = OpenModalViewModel;
            OpenModal.ShowDialog(); // displays the viewModel
          
        }

        private ObservableCollection<string> _joinedChats;
        public ObservableCollection<string> JoinedChats
        {
            get { return _joinedChats; }
            set 
            {
                _joinedChats = value;
                OnProperyChanged(nameof(JoinedChats));
            }
        }

        private string _userBio;
        public string UserBio
        {
            get { return _userBio; }
            set { _userBio = value; }
        }



        public UserProfileViewModel(NavStore navStore, AccountUser sessionUser)
        {
            _navStore = navStore;
            _sessionUser = sessionUser;

            JoinedChats = new ObservableCollection<string>();
            using (MasterContext context =  new MasterContext())
            {
                var currentUser = context.AccountUsers.Single(au => au.Id == sessionUser.Id); // queries database for current users' data
                AccountUsername = currentUser.Username;
                UserBio = currentUser.Bio;
                if (currentUser.ProfileImageUrl == null)
                {
                    //sets default profile picture (taken from a stock site)
                    ProfilePictureUrl = "https://play-lh.googleusercontent.com/z-ppwF62-FuXHMO7q20rrBMZeOnHfx1t9UPkUqtyouuGW7WbeUZECmyeNHAus2Jcxw=w526-h296-rw";
                }
                else
                {
                    ProfilePictureUrl = currentUser.ProfileImageUrl; 
                }

                var UserchatRelation = context.AccountUserGroupChats
                    .Where(cr => cr.AccountUserId == sessionUser.Id).ToList(); // queries database for the Ids of all GroupChat the user is in 

                foreach (var link in UserchatRelation)
                {
                    var chat = context.GroupChats.Single(c => c.Id == link.GroupChatId).Name.ToString(); // queries database for the names of the groupchats that
                                                                                                        // the user is in.
                    JoinedChats.Add(chat);
                }
                ObservableCollection<string> temp = new ObservableCollection<string>(); // creates a temporary list which will be moved to another property before being displayed.
                OnProperyChanged(nameof(OnProperyChanged));
            }
            NavigateMainAppCommand = new NavigateMainApp(navStore, sessionUser);
            OpenEditProfilePopup = new OpenEditProfileModal(this, _sessionUser);
        }
    }
}
