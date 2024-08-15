using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NEA_Main.Models;
using NEA_Main.Data;

namespace NEA_Main.ViewModels
{
    class AppViewModel : ViewModelBase
    {
        public AccountUser SessionUser { get; set; }

        public AppViewModel(NavStore navStore, AccountUser sessionUser)
        {
            SessionUser = sessionUser;

        }
    }
}
