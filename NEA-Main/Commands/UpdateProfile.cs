using NEA_Main.Models.Generated;
using NEA_Main.ViewModels;
using NEA_Main.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA_Main.Data;
using NEA_Main.Models;
using Microsoft.EntityFrameworkCore;

namespace NEA_Main.Commands
{
    public class UpdateProfile : CommandBase
    {
        private readonly EditProfileViewModel _parentViewModel;
        private readonly AccountUser _sessionUser;
        
        public UpdateProfile(EditProfileViewModel parentViewModel, AccountUser sessionUser)
        {
            _parentViewModel = parentViewModel;
            _sessionUser = sessionUser;
        }
        public override void Execute(object? parameter)
        {
           using (MasterContext context = new MasterContext())
           {
                var TargetUser = context.AccountUsers.Single(au => au.Id == _sessionUser.Id); // queries the database for current user
                TargetUser.ProfileImageUrl = _parentViewModel.NewProfilePictureUrl; // edits the user's record, changing their profile picture url
                context.SaveChanges();

                _parentViewModel.OutputResult("Profile picture updated");

            }
        }
    }
}
