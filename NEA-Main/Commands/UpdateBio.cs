using NEA_Main.Data;
using NEA_Main.Models.Generated;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class UpdateBio : CommandBase
    {
        private readonly EditProfileViewModel _parentViewModel;
        private readonly AccountUser _sessionUser;
        
        public UpdateBio(EditProfileViewModel parentViewModel, AccountUser sessionUser)
        {
            _parentViewModel = parentViewModel;
            _sessionUser = sessionUser;
        }

        public override void Execute(object? parameter)
        {
           using (MasterContext context = new MasterContext())
           {
                var TargetUser = context.AccountUsers.Single(au => au.Id == _sessionUser.Id);
                TargetUser.Bio = _parentViewModel.NewBio;
                context.SaveChanges();

                _parentViewModel.OutputResult("Bio Updated");

            }
        }
    }
}
