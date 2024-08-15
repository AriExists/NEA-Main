using NEA_Main.Models;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class RegisterAccountCommand : CommandBase
    {
        private readonly CreateAccountViewModel _createAccountViewModel;
      

        public RegisterAccountCommand(CreateAccountViewModel createAccountViewModel)
        {
            _createAccountViewModel = createAccountViewModel;
     
        }
        public override void Execute(object? parameter)
        {
            _createAccountViewModel.TryCreateAccount();
        }
    }
}
