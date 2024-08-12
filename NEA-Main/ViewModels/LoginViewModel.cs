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
    class LoginViewModel : ViewModelBase
    {
        public ICommand? NavigateStart { get; }
        

        public LoginViewModel(NavStore navStore)
        {
            NavigateStart = new NavigateStartCommand(navStore);
        } 

    }
}
