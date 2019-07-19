using Acr.UserDialogs;
using ProteusMMX.Controls;
using ProteusMMX.Crypto;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
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
    public partial class CloseWorkorderSignaturePage : PopupPage
    {
        string password = string.Empty;
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string FacilityID = string.Empty;
        string Workorderid = string.Empty;
        string WONavigation = string.Empty;
        workOrders wowrapper = null;
        ServiceOutput user;
        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;

      
        public CloseWorkorderSignaturePage()
        {
            InitializeComponent();
            HeaderLabel.Text = WebControlTitle.GetTargetNameByTitleName("Signature") + " " + WebControlTitle.GetTargetNameByTitleName("For") + " " + AppSettings.User.UserName + " " + WebControlTitle.GetTargetNameByTitleName("On") + " " + WebControlTitle.GetTargetNameByTitleName("WorkOrders");
            if (Application.Current.Properties.ContainsKey("UserNameType"))
            {
                string UsernameType = Application.Current.Properties["UserNameType"].ToString();
                if (UsernameType == "TextBox")
                {
                    Application.Current.Properties["UserNameType"] = "TextBox";
                    UserLabel.Text = "";
                    UserLabel.IsEnabled = true;
                }
                else
                {
                    Application.Current.Properties["UserNameType"] = "Label";
                    UserLabel.Text = AppSettings.User.UserName;
                    UserLabel.IsEnabled = false;
                }
            }
            Username.Text = WebControlTitle.GetTargetNameByTitleName("UserName");
            Pass.Text = WebControlTitle.GetTargetNameByTitleName("Password");
            ok.Text = WebControlTitle.GetTargetNameByTitleName("OK");

        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        private async void OK_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                if (Application.Current.Properties.ContainsKey("Password"))
                {
                    password = Application.Current.Properties["Password"].ToString();

                }
                if (AppSettings.User.UserName.ToLower() != UserLabel.Text.ToLower() || password != PassLabel.Text)
                {
                    UserDialogs.Instance.HideLoading();
                    await DialogService.ShowAlertAsync(WebControlTitle.GetTargetNameByTitleName("InvalidCredentials"), WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    return;
                }
                if (Application.Current.Properties.ContainsKey("SignatureFacID"))
                {
                    try
                    {
                        FacilityID = Application.Current.Properties["SignatureFacID"].ToString();
                    }
                    catch (Exception)
                    {

                        FacilityID = null;
                    }
                    

                }
                if (Application.Current.Properties.ContainsKey("SignatureWOID"))
                {
                    try
                    {
                        Workorderid = Application.Current.Properties["SignatureWOID"].ToString();
                    }
                    catch (Exception)
                    {

                        Workorderid = null;
                    }
                   

                }
                if (Application.Current.Properties.ContainsKey("WorkorderNavigation"))
                {
                    WONavigation = Application.Current.Properties["WorkorderNavigation"].ToString();

                }
                var workorder = new workOrderWrapper
                {
                    TimeZone = AppSettings.UserTimeZone,
                    CultureName = AppSettings.UserCultureName,
                    UserId = Convert.ToInt32(AppSettings.User.UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    workOrder = new workOrders
                    {
                        IsSignatureValidated = true,
                        FacilityID =Convert.ToInt32(FacilityID),
                        WorkOrderID = Convert.ToInt32(Workorderid),
                        ModifiedUserName = AppSettings.User.UserName,

                    },

                };


                var response = await WorkorderService.CloseWorkorder(workorder);

                if (Boolean.Parse(response.servicestatus))
                {
                    Application.Current.Properties["UserNameType"] = "Label";
                    SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                    if (WONavigation == "True")
                    {
                       
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                        await navigationService.NavigateBackAsync();
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        await Navigation.PopAsync();
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                        await navigationService.NavigateToAsync<WorkorderListingPageViewModel>();
                        UserDialogs.Instance.HideLoading();
                    }
                  
                   
                   
                }
                else
                {
                    DialogService.ShowToast(response.servicestatusmessge, 2000);
                    UserDialogs.Instance.HideLoading();
                    return;


                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (AppSettings.User.UserName.ToLower() != UserLabel.Text.ToLower() || password != PassLabel.Text)
                {
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    OnClose(null, null);
                    UserDialogs.Instance.HideLoading();
                }
            }

        }


    }

}