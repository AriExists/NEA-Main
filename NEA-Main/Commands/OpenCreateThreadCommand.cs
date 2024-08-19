using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using NEA_Main.Views;

namespace NEA_Main.Commands
{
    public class OpenCreateThreadCommand : CommandBase
    {
        private readonly AppViewModel _viewModel;
        private readonly Window _modal;
        private readonly ViewModelBase _modalViewModal;
        public OpenCreateThreadCommand(AppViewModel viewModel)
        {
            _viewModel = viewModel;
            _modal = new CreateThreadPopup();
            _modalViewModal = new CreateThreadViewModel(_viewModel);
        }

        public override void Execute(object? parameter)
        {
            _viewModel.OpenNewModal(_modal, _modalViewModal);
        }
    }
}
