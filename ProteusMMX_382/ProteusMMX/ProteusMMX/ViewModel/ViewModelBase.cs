using ProteusMMX.Services.Connectivity;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public bool _operationInProgress;
        public bool OperationInProgress
        {
            get
            {
                return _operationInProgress;
            }

            set
            {

                if (value != _operationInProgress)
                {
                    _operationInProgress = value;
                    OnPropertyChanged("OperationInProgress");

                }
            }
        }

        Page _page;
        public Page Page
        {
            get
            {
                return _page;
            }

            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName)); //new PropertyChangedEventArgs(propertyName));
            }
        }


        protected readonly INavigationService NavigationService;
        protected readonly IConnectivityService ConnectivityService;
        protected readonly IDialogService DialogService;


        public ViewModelBase()
        {
            DialogService = Locator.Instance.Resolve<IDialogService>();
            NavigationService = Locator.Instance.Resolve<INavigationService>();
            ConnectivityService = Locator.Instance.Resolve<IConnectivityService>();
        }



        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

    }
}
