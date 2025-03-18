using Microsoft.Identity.Client;
using NEA_Main.Commands;
using NEA_Main.Models;
using NEA_Main.Models.Generated;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace NEA_Main.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {

        public ICommand ChangeThemeCmd { get; set; }
        public ICommand NavigateStart { get; set; }
       

       
        public SettingsViewModel(NavStore navStore, AccountUser sessionUser) 
        {
            ChangeThemeCmd = new ChangeThemeCommand(this);
            NavigateStart = new NavigateMainApp(navStore, sessionUser);
        }

        public void ChangeTheme(object themeButtonPressed)
        {
            switch (Convert.ToString(themeButtonPressed)) // input depends on the theme button pressed in view
                                                        // changes the resource dictionary used for colouring UI elements
            {
                case "Azure":
                    System.Windows.Application.Current.Resources.Source = new Uri("pack://application:,,,/Styles/AzureTheme.xaml");        
                    break;
                case "Maroon":
                    System.Windows.Application.Current.Resources.Source = new Uri("pack://application:,,,/Styles/MainTheme.xaml");
                    break;
                case "Haze":
                    System.Windows.Application.Current.Resources.Source = new Uri("pack://application:,,,/Styles/HazeTheme.xaml");
                    break;
                case "Night":
                    System.Windows.Application.Current.Resources.Source = new Uri("pack://application:,,,/Styles/NightTheme.xaml");
                    break;
                case "Moss":
                    System.Windows.Application.Current.Resources.Source = new Uri("pack://application:,,,/Styles/MossTheme.xaml");
                    break;
                case "Sunburn":
                    System.Windows.Application.Current.Resources.Source = new Uri("pack://application:,,,/Styles/SunburnTheme.xaml");
                    break;

            }
        }

    }
}
