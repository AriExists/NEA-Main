using NEA_Main.Commands;
using NEA_Main.Data;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Data;
using NEA_Main.Models;
using System.Windows.Controls.Primitives;
using NEA_Main.Models.Generated;

namespace NEA_Main.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public ICommand? NavigateStart { get; }
        public ICommand? TryLoginCommand { get; }
        private readonly NavStore _navStore;

        private string? _inputUserName;
        public string? InputUserName
        {
            get  =>  _inputUserName; 
            set
            {
                _inputUserName = value;
                OnProperyChanged(nameof(InputUserName));
            }
        }
        private string? _inputPassword;
        public string? InputPassword
        {
            get => _inputPassword;
            set
            {
                _inputPassword = value;
                OnProperyChanged(nameof(InputPassword));
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

        public LoginViewModel(NavStore navStore)
        {
            NavigateStart = new NavigateStartCommand(navStore);
            _navStore = navStore;
            TryLoginCommand = new LoginCommand(this, _navStore);
        }

        MasterContext context = new MasterContext();
        
        public AccountUser? TryLogin()
        {
            if (!string.IsNullOrEmpty(InputPassword) && !string.IsNullOrEmpty(InputUserName))
            {
                foreach (AccountUser u in context.AccountUsers)
                {
                    if (u.Username == InputUserName)
                    {
                        if (u.Password == InputPassword)
                        {   
                            return u;
                        }
                    }
                }
                Result = "Incorrect username or password";
                return null;
            }
            return null;
        }
        

        //public void CompleteLogin(AccountUser user)
        //{
            
        //    SessionUserStore sessionUser = new SessionUserStore(user);
        //    _navStore.CurrentViewModel = new LoginViewModel(_navStore);

        //}
    }
}
