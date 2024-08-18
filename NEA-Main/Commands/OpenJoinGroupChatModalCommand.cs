using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    class OpenJoinGroupChatModalCommand : CommandBase
    {

        private readonly AppViewModel _viewModel;
        public OpenJoinGroupChatModalCommand(AppViewModel viewModel)
        {
            _viewModel = viewModel;
        }


        public override void Execute(object? parameter)
        {
            _viewModel.OpenChangeGroupChatModal();
        }
    }
}
