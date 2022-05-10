﻿using Acr.UserDialogs;
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
    public partial class CreateWorkorderSignaturePage : PopupPage
    {
        string password = string.Empty;
        IDialogService DialogService = Locator.Instance.Resolve<IDialogService>();
        IWorkorderService WorkorderService = Locator.Instance.Resolve<IWorkorderService>();
        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        INavigationService navigationService = Locator.Instance.Resolve<INavigationService>();
        string CauseID = string.Empty;
        workOrders wowrapper = null;
        Cause CauseJson;
        public event EventHandler<SignaturePageModel> OnSignatureDrawn;
        CustomImage imageview;
        ServiceOutput user;
        public CreateWorkorderPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as CreateWorkorderPageViewModel;
            }
        }
        public CreateWorkorderSignaturePage()
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
                if (Application.Current.Properties.ContainsKey("CauseID") && Application.Current.Properties["CauseID"] != null)
                {
                  
                    try
                    {
                        CauseID = Application.Current.Properties["CauseID"].ToString();
                    }
                    catch (Exception)
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
                    var response = await WorkorderService.CreateWorkorder(workorder);
                    if (response != null && bool.Parse(response.servicestatus.ToString()))
                    {
                        Application.Current.Properties["UserNameType"] = "Label";
                        SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                        await navigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
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
                    var response = await WorkorderService.CreateWorkorder(workorder);
                    if (response != null && bool.Parse(response.servicestatus.ToString()))
                    {
                        Application.Current.Properties["UserNameType"] = "Label";
                        SignatureStorage.Storage.Set("FDASignatureUserValidated", "True");
                        await navigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                        await navigationService.RemoveLastFromBackStackAsync();
                    }
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