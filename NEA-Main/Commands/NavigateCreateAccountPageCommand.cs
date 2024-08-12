using NEA_Main.Stores;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class NavigateCreateAccountPageCommand : CommandBase
    {
        private readonly NavStore _navStore;

        public NavigateCreateAccountPageCommand(NavStore navStore)
        {
            _navStore = navStore;
        }

        public override void Execute(object? parameter)
        {
            _navStore.CurrentViewModel = new CreateAccountViewModel(_navStore);
        }
        
    }
}
