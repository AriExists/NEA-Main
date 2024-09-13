using NEA_Main.ViewModels;
using NEA_Main.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NEA_Main.Commands
{
    public class OpenUserListCommand : CommandBase
    {

        private readonly AppViewModel _viewModel;
  
        private readonly ViewModelBase _modalViewModal;
        public OpenUserListCommand(AppViewModel viewModel)
        {
            _viewModel = viewModel;
            _modalViewModal = new UserListViewModel(_viewModel);
        }


        public override void Execute(object? parameter)
        {
            var _modal = new UserListPopup();
            _viewModel.OpenNewModal(_modal, _modalViewModal);

        }


    }
}
