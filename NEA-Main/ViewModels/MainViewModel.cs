using NEA_Main.Commands;
using NEA_Main.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly NavStore _navsStore;

        public ViewModelBase? CurrentViewModel => _navsStore.CurrentViewModel;

        public MainViewModel(NavStore navStore)
        {
            _navsStore = navStore;
            _navsStore.CurrentViewModleChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnProperyChanged(nameof(CurrentViewModel));
        }
    }
}
