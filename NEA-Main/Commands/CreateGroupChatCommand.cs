﻿using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class CreateGroupChatCommand : CommandBase
    {

        private readonly CreateGroupChatViewModel _viewModel;
        public CreateGroupChatCommand(CreateGroupChatViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object? parameter)
        {
            _viewModel.TryCreateGroupChat();
        }
    }
}
