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
    	// property setup
    public ICommand EditProfileCommand { get; set; }
		public ICommand EditBioCommand { get; set; }
		private string? newProfilePictureUrl;

		public string NewProfilePictureUrl
		{
			get  // returns empty string rather than null to avoid crashes when making comparisons
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

		private string _newBio;

		public string NewBio
		{
			get { return _newBio; }
			set 
			{
				_newBio = value;
				OnProperyChanged(nameof(NewBio));
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
      public EditProfileViewModel(AccountUser sessionUser) // constructor instantiates the commands for buttons
      {
        _sessionUser = sessionUser;
        EditProfileCommand = new UpdateProfile(this, _sessionUser);
				EditBioCommand = new UpdateBio(this, _sessionUser);
      }



		public void OutputResult(string result)
		{
            Result = result;
		}
	}
}
