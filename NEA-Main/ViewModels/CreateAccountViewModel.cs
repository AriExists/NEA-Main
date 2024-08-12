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
    class CreateAccountViewModel : ViewModelBase
    {
        public ICommand? NavigateStart { get; }

        public CreateAccountViewModel(NavStore navStore)
        {
            NavigateStart = new NavigateStartCommand(navStore);
        }
    }
}
