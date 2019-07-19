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
using ProteusMMX.ViewModel.Miscellaneous;
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
    public partial class EditWorkorderSignaturePage : PopupPage
    {
        string password = string.Empty;
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string CauseID = string.Empty;
        string woid = string.Empty;
        workOrders wowrapper = null;
        Cause CauseJson;
        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;
        EditWorkorderPageViewModel ViewModel;
        ServiceOutput user;

        public EditWorkorderSignaturePage()
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
                if (Application.Current.Properties.ContainsKey("CauseID"))
                {
                    try
                    {
                        CauseID = Application.Current.Properties["CauseID"].ToString();
                    }
                    catch (Exception ex)
                    {

                        CauseID = null;
                    }
                    

                }
                
                if (Application.Current.Properties.ContainsKey("CauseJson"))
                {
                    try
                    {
                        CauseJson = Application.Current.Properties["CauseJson"] as Cause;
                    }
                    catch (Exception)
                    {

                        CauseJson = null;
                    }
                   

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
                    var response = await WorkorderService.UpdateWorkorder(workorder);
                    if (response != null && bool.Parse(response.servicestatus.ToString()))
                    {

                    
                        Application.Current.Properties["UserNameType"] = "Label";
                        SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated."), 2000);
                        // var workorderWrapper1 = await WorkorderService.GetWorkorderByWorkorderID(AppSettings.User.UserID.ToString(), wowrapper.WorkOrderID.ToString());
                        //// await ViewModel.SetControlsPropertiesForPage(workorderWrapper1);
                        await navigationService.NavigateBackAsync();


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
                    var response = await WorkorderService.UpdateWorkorder(workorder);
                    if (response != null && bool.Parse(response.servicestatus.ToString()))
                    {

                        Application.Current.Properties["UserNameType"] = "Label";
                        SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated."), 2000);
                        await navigationService.NavigateBackAsync();
                    }
                }
            }
            catch (Exception ex)
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