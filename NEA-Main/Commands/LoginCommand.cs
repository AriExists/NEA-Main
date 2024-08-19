using NEA_Main.Stores;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA_Main.Models;
using NEA_Main.Models.Generated;

namespace NEA_Main.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private readonly NavStore _navStore;
        private AccountUser? _sessionUserStore;
        public LoginCommand(LoginViewModel loginViewModel, NavStore navStore)
        {
            _viewModel = loginViewModel;
            _navStore = navStore;
            

        }
        public override void Execute(object? parameter)
        {
            _sessionUserStore = _viewModel.TryLogin();
            if (_sessionUserStore != null)
            {
                _navStore.CurrentViewModel = new AppViewModel(_navStore, _sessionUserStore);
            }
            
        }
    }
}
