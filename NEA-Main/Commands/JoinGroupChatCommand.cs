using NEA_Main.Models;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    class JoinGroupChatCommand : CommandBase
    {
        private readonly AppViewModel _parentViewModel;
        private readonly string? _joinCode;
        private readonly JoinGroupChatViewModel _parentModal;
        public JoinGroupChatCommand(AppViewModel viewModel, JoinGroupChatViewModel parentModal)
        {
            _parentViewModel = viewModel;  
            _parentModal = parentModal;
            _joinCode = _parentModal.InputJoinCode;
        }
        private OutputResult _out;
        public override void Execute(object? parameter)
        {
            _out = _parentViewModel.JoinGroupChat(_parentModal.InputJoinCode);
            _parentModal.Output(_out);
            _parentViewModel.updateGroupChatSelector();

        }
    }
}

