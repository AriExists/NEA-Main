using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;
using NEA_Main.Commands;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Data;
using NEA_Main.Models;
using System.Drawing;
using System.Windows.Media;
using NEA_Main.Models.Generated;

namespace NEA_Main.ViewModels
{

    public class CreateAccountViewModel : ViewModelBase
    {
        public ICommand? NavigateStart { get; }
        public ICommand? RegisterCommand { get; }
        
        internal MasterContext context = new MasterContext(); // creating server context object

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

        private string? _inputUsername;
        public string? InputUsername
        {
            get => _inputUsername;
            set
            {
                _inputUsername = value;
                OnProperyChanged(nameof(InputUsername));
            }
        }

        private string? _inputConfirmPassword;
        public string? InputConfirmPassword
        {
            get => _inputConfirmPassword;
            set
            {
                _inputConfirmPassword = value;
                OnProperyChanged(nameof(_inputConfirmPassword));
            }
        }

        public CreateAccountViewModel(NavStore navStore)
        {
            NavigateStart = new NavigateStartCommand(navStore);
            RegisterCommand = new RegisterAccountCommand(this);
            ResponseColour = System.Windows.Media.Brushes.Red;

        }

        public int TryCreateAccount()
        {
            if (!string.IsNullOrEmpty(InputUsername) && !string.IsNullOrEmpty(InputPassword) && !string.IsNullOrEmpty(InputConfirmPassword))
            {
                if (!string.Equals(InputConfirmPassword.ToLower(), InputPassword.ToLower())) 
                {
                    ResponseColour = System.Windows.Media.Brushes.Red;
                    Result = "The password and confirm password do not match";
                    return -1;
                }
                //Start of database editing code
                foreach (AccountUser u in context.AccountUsers)  //Check other users in database to avoid duplicate usernames
                {
                    if (string.Equals(u.Username.ToLower(), InputUsername.ToLower()))
                    {
                        Result = "That username is already taken";
                        return -1;
                    }
                }
                AccountUser newUser = new AccountUser()
                {
                    Username = InputUsername,
                    Password = InputPassword
                };
                context.Add(newUser);
                context.SaveChanges();
                // end of database editing code
                ResponseColour = System.Windows.Media.Brushes.Green;
                Result = "Account created";
            }
            else
            {
                ResponseColour = System.Windows.Media.Brushes.Red;
                Result = "Please fill in all of the info";
            }

            return 0;

            
        }
    }
}
