using NEA_Main.Commands;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NEA_Main.ViewModels
{
    class StartViewModel : ViewModelBase
    {
        public ICommand? NavigateLoginPage { get; }
        public ICommand? NavigateCreateAccountPage { get; }

        public StartViewModel(NavStore navstore)
        {
            NavigateCreateAccountPage = new NavigateCreateAccountPageCommand(navstore);
            NavigateLoginPage = new NavigateLoginPageCommand(navstore);
        }

    }
}
