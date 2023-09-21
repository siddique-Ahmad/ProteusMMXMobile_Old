using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model.ServiceRequestModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.ServiceRequest;
using ProteusMMX.Services.Workorder;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.ServiceRequest;
using ProteusMMX.Views.ServiceRequest;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SRDecline : PopupPage
    {


        IServiceRequestModuleService ServiceRequestService = Locator.Instance.Resolve<IServiceRequestModuleService>();
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        protected readonly IServiceRequestModuleService _serviceRequestService;

        public SRDecline(string UserID, int? ServiceRequestID, IServiceRequestModuleService serviceRequestService)
        {
            InitializeComponent();
            this.ServiceRequestID = ServiceRequestID;
            this.UserID = UserID;
            _serviceRequestService = serviceRequestService;
        }

        int? _serviceRequestID;
        public int? ServiceRequestID
        {
            get
            {
                return _serviceRequestID;
            }

            set
            {
                if (value != _serviceRequestID)
                {
                    _serviceRequestID = value;
                    OnPropertyChanged(nameof(ServiceRequestID));
                }
            }
        }

        string _userID = AppSettings.User.UserID.ToString();
        public string UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //OnDispose?.Invoke();
        }

        private async void OK_Clicked(object sender, EventArgs e)
        {

            var yourobject = new ServiceRequests
            {


                UserId = Convert.ToInt32(this.UserID),
                ServiceRequestID = this.ServiceRequestID,


            };


            var response = await _serviceRequestService.DeclineServiceRequest(yourobject);

            if (Boolean.Parse(response.servicestatus))
            {
                await PopupNavigation.PopAsync();
                await Application.Current.MainPage.Navigation.PopAsync();
               
            }
            UserDialogs.Instance.HideLoading();

        }
        private async void CANCEL_Clicked(object sender, EventArgs e)
        {

            await PopupNavigation.PopAsync();
        }
       

        
    }
}