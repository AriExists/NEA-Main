using NEA_Main.Models.Generated;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA_Main.Data;
using NEA_Main.Models;


namespace NEA_Main.Commands
{
    public class RefreshUserListCommand : CommandBase
    {
        private MasterContext context = new MasterContext();
        private readonly AppViewModel _parentViewModel;
        private UserListViewModel _targetModal;

        public RefreshUserListCommand(AppViewModel parentViewModel, UserListViewModel targetModal)
        {
            _parentViewModel = parentViewModel;
            _targetModal = targetModal;
        }

        public override void Execute(object? parameter)
        {
            _targetModal.RefreshUsers();
        }
    }
}
