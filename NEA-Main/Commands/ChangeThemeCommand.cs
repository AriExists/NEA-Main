using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Commands
{
    public class ChangeThemeCommand : CommandBase
    {
        private readonly SettingsViewModel _parentViewModel;
        public ChangeThemeCommand(SettingsViewModel parentViewModel)
        {
            _parentViewModel = parentViewModel;
        }
        public override void Execute(object? parameter)
        {
            _parentViewModel.ChangeTheme(parameter);
        }
    }
}
