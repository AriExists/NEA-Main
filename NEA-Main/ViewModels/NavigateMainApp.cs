﻿using NEA_Main.Commands;
using NEA_Main.Models;
using NEA_Main.Models.Generated;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This file is supposed to be in the commands namespace but I put it here by accident
// I didnt change it as many files reference it at this location, which would be tedious to fix
namespace NEA_Main.ViewModels
{
    class NavigateMainApp : CommandBase
    {
        private readonly NavStore _navStore;
        private readonly AccountUser _sessionUser;
        public NavigateMainApp(NavStore navStore, AccountUser sessionUser)
        {
            _navStore = navStore;
            _sessionUser = sessionUser;
        }

        public override void Execute(object? parameter)
        {
            _navStore.CurrentViewModel = new AppViewModel(_navStore, _sessionUser); // navigates the user to the main app
        }
    }
}
