using NEA_Main.Commands;
using NEA_Main.Models.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NEA_Main.ViewModels
{
    public class EditProfileViewModel : ViewModelBase
    {
        public ICommand EditProfileCommand { get; set; }
		private string? newProfilePictureUrl;

		public string NewProfilePictureUrl
		{
			get 
			{
				if (newProfilePictureUrl != null)
				{
					return newProfilePictureUrl;
				}
				return "";
			}
			set 
			{
				newProfilePictureUrl = value; 
				OnProperyChanged(nameof(NewProfilePictureUrl));
			}
		}

        private string _result;

        public string Result    
        {
            get { return _result; }
            set
            {
                _result = value;
                OnProperyChanged(nameof(Result));
            }
        }
        private readonly AccountUser _sessionUser;
        public EditProfileViewModel(AccountUser sessionUser)
        {
            _sessionUser = sessionUser;
            EditProfileCommand = new UpdateProfile(this, _sessionUser);
        }



		public void OutputResult(string result)
		{
            Result = result;
		}
	}
}
