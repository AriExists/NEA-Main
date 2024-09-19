using NEA_Main.Models.Generated;
using NEA_Main.ViewModels;
using NEA_Main.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class OpenEditProfileModal : CommandBase
    {
        private readonly UserProfileViewModel _viewmodel;
        private EditProfileViewModel editProfileViewModel;

        public OpenEditProfileModal(UserProfileViewModel viewmodel, AccountUser sessionUser)
        {
            _viewmodel = viewmodel;
            editProfileViewModel = new EditProfileViewModel(sessionUser);
        }

        public override void Execute(object? parameter)
        {    
            var _modal = new EditProfileModal();
            _viewmodel.OpenNewModal(_modal, editProfileViewModel);
        }

    }
}
