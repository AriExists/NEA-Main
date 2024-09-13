using NEA_Main.Models.Generated;
using NEA_Main.Stores;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class NavigateSettingsCommand : CommandBase
    {
        private readonly NavStore _navStore;
        private readonly AccountUser _sessionUser;
        public NavigateSettingsCommand(NavStore navStore, Models.Generated.AccountUser sessionUser)
        {
            _navStore = navStore;
            _sessionUser = sessionUser;
            
        }
        public override void Execute(object? parameter)
        {
            _navStore.CurrentViewModel = new SettingsViewModel(_navStore, _sessionUser);
        }
    }
}
