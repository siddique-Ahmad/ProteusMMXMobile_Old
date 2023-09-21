using Acr.UserDialogs;
using ProteusMMX.Controls;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Workorder;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SignaturePad.Forms;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class CustomSignaturePage : PopupPage
    {
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string CauseID = string.Empty;
        workOrders wowrapper = null;
        Cause CauseJson;
        CustomImage imageview;
        public CustomSignaturePage()
        {
            InitializeComponent();
            //this.imageview = imageview;
        }

      
        private async Task OK_Clicked(object sender, EventArgs e)

        {
            try
            {
                if (Application.Current.Properties.ContainsKey("CauseID") && Application.Current.Properties["CauseID"] != null)
                {
                    CauseID = Application.Current.Properties["CauseID"].ToString();

                }
                if (Application.Current.Properties.ContainsKey("CauseJson"))
                {
                    CauseJson = Application.Current.Properties["CauseJson"] as Cause;

                }
                if (Application.Current.Properties.ContainsKey("WorkorderWrapper"))
                {
                    wowrapper = Application.Current.Properties["WorkorderWrapper"] as workOrders;

                }
                if (CauseID == null)
                {
                    var workorder = new workOrderWrapper
                    {
                        TimeZone = AppSettings.UserTimeZone,
                        CultureName = AppSettings.UserCultureName,
                        UserId = Convert.ToInt32(AppSettings.User.UserID),
                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        workOrder = wowrapper,
                        cause = null,


                    };
                    var response = WorkorderService.CreateWorkorder(workorder);
                    if (response != null && bool.Parse(response.Status.ToString()))
                    {
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                       await navigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.Result.workOrderWrapper.workOrder);
                       await navigationService.RemoveLastFromBackStackAsync();

                    }
                }
                else
                {

                    var workorder = new workOrderWrapper
                    {
                        TimeZone = AppSettings.UserTimeZone,
                        CultureName = AppSettings.UserCultureName,
                        UserId = Convert.ToInt32(AppSettings.User.UserID),
                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        workOrder = wowrapper,
                        cause = CauseJson,


                    };
                    var response = WorkorderService.CreateWorkorder(workorder);
                    if (response != null && bool.Parse(response.Status.ToString()))
                    {
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                       await navigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.Result.workOrderWrapper.workOrder);
                       await  navigationService.RemoveLastFromBackStackAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                OnClose(null, null);
                UserDialogs.Instance.HideLoading();
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

    }
}



            
          