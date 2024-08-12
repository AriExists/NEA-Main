using System.Configuration;
using System.Data;
using System.Windows;
using NEA_Main.Stores;
using NEA_Main.ViewModels;

namespace NEA_Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavStore navStore = new NavStore();
            navStore.CurrentViewModel = new StartViewModel(navStore);
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
    

}
