using NEA_Main.Stores;
using NEA_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NEA_Main.Views
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : UserControl
    {
        private NavStore _store;
        private CreateAccountViewModel _viewModel;
        public CreateAccountWindow()
        {
            InitializeComponent();
            _store = new NavStore();
            _viewModel = new CreateAccountViewModel(_store);
        }



        private string? _usernameInput;
        private string? _passwordInput;
        private string? _confirmPasswordInput;
        private int _exitCode;
        public int ExitCode { get; set; } 
        //private void Register_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    _usernameInput = UsernameInput.Text;
        //    _passwordInput = PasswordInput.Text;
        //    _confirmPasswordInput = ConfirmPasswordInput.Text;
         
        //    _exitCode = _viewModel.TryCreateAccount(_usernameInput, _passwordInput, _confirmPasswordInput);
        //}
    }
}
