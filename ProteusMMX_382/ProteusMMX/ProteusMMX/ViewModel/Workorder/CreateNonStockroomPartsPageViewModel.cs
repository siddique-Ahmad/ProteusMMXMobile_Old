using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace ProteusMMX.ViewModel.Workorder
{
    public class CreateNonStockroomPartsPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;
        #endregion

        #region Properties

        #region Page Properties

        string _pageTitle = "";
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }

            set
            {
                if (value != _pageTitle)
                {
                    _pageTitle = value;
                    OnPropertyChanged("PageTitle");
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

        ServiceOutput _formLoadInputForWorkorder;
        public ServiceOutput FormLoadInputForWorkorder //Use For Only translation purposes
        {
            get { return _formLoadInputForWorkorder; }
            set
            {
                if (value != _formLoadInputForWorkorder)
                {
                    _formLoadInputForWorkorder = value;
                    OnPropertyChanged(nameof(FormLoadInputForWorkorder));
                }
            }
        }








        #endregion

        #region Title Properties
        string _saveTitle;
        public string SaveTitle
        {
            get
            {
                return _saveTitle;
            }

            set
            {
                if (value != _saveTitle)
                {
                    _saveTitle = value;
                    OnPropertyChanged(nameof(SaveTitle));
                }
            }
        }
        string _partNametitle;
        public string PartNameTitle
        {
            get
            {
                return _partNametitle;
            }

            set
            {
                if (value != _partNametitle)
                {
                    _partNametitle = value;
                    OnPropertyChanged("PartNameTitle");
                }
            }
        }

        string _partNumbertitle;
        public string PartNumberTitle
        {
            get
            {
                return _partNumbertitle;
            }

            set
            {
                if (value != _partNumbertitle)
                {
                    _partNumbertitle = value;
                    OnPropertyChanged("PartNumberTitle");
                }
            }
        }

        string _quantityRequiredtitle;
        public string QuantityRequiredtitle
        {
            get
            {
                return _quantityRequiredtitle;
            }

            set
            {
                if (value != _quantityRequiredtitle)
                {
                    _quantityRequiredtitle = value;
                    OnPropertyChanged("QuantityRequiredtitle");
                }
            }
        }

        string _unitCosttitle;
        public string UnitCosttitle
        {
            get
            {
                return _unitCosttitle;
            }

            set
            {
                if (value != _unitCosttitle)
                {
                    _unitCosttitle = value;
                    OnPropertyChanged("UnitCosttitle");
                }
            }
        }

        string _logoutTitle = "";
        public string LogoutTitle
        {
            get
            {
                return _logoutTitle;
            }

            set
            {
                if (value != _logoutTitle)
                {
                    _logoutTitle = value;
                    OnPropertyChanged("LogoutTitle");
                }
            }
        }
        string _welcomeTextTitle;
        public string WelcomeTextTitle
        {
            get
            {
                return _welcomeTextTitle;
            }

            set
            {
                if (value != _welcomeTextTitle)
                {
                    _welcomeTextTitle = value;
                    OnPropertyChanged("WelcomeTextTitle");
                }
            }
        }




        #endregion

        #region Dialog Actions Titles


        string _selectTitle = "";
        public string SelectTitle
        {
            get
            {
                return _selectTitle;
            }

            set
            {
                if (value != _selectTitle)
                {
                    _selectTitle = value;
                    OnPropertyChanged("SelectTitle");
                }
            }
        }

        string _cancelTitle = "";
        public string CancelTitle
        {
            get
            {
                return _cancelTitle;
            }

            set
            {
                if (value != _cancelTitle)
                {
                    _cancelTitle = value;
                    OnPropertyChanged("CancelTitle");
                }
            }
        }

        string _selectOptionsTitle ;
        public string SelectOptionsTitle
        {
            get
            {
                return _selectOptionsTitle;
            }

            set
            {
                if (value != _selectOptionsTitle)
                {
                    _selectOptionsTitle = value;
                    OnPropertyChanged("SelectOptionsTitle");
                }
            }
        }


        #endregion

        #endregion



        #region CreateNonStockroomParts Properties





        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }


        #region Normal Field Properties


        int _workorderID;
        public int WorkorderID
        {
            get
            {
                return _workorderID;
            }

            set
            {
                if (value != _workorderID)
                {
                    _workorderID = value;
                    OnPropertyChanged(nameof(WorkorderID));
                }
            }
        }


        string _partNameText;
        public string PartNameText
        {
            get
            {
                return _partNameText;
            }

            set
            {
                if (value != _partNameText)
                {
                    _partNameText = value;
                    OnPropertyChanged(nameof(PartNameText));
                }
            }
        }

        string _partNumberText;
        public string PartNumberText
        {
            get
            {
                return _partNumberText;
            }

            set
            {
                if (value != _partNumberText)
                {
                    _partNumberText = value;
                    OnPropertyChanged(nameof(PartNumberText));
                }
            }
        }


        string _quantityRequiredText;
        public string QuantityRequiredText
        {
            get
            {
                return _quantityRequiredText;
            }

            set
            {
                if (value != _quantityRequiredText)
                {
                    _quantityRequiredText = value;
                    OnPropertyChanged(nameof(QuantityRequiredText));
                }
            }
        }

        string _unitCostText;
        public string UnitCostText
        {
            get
            {
                return _unitCostText;
            }

            set
            {
                if (value != _unitCostText)
                {
                    _unitCostText = value;
                    OnPropertyChanged(nameof(UnitCostText));
                }
            }
        }


        #endregion


        #endregion





        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        //Save Command
        public ICommand SaveNonStockroomPartsCommand => new AsyncCommand(SaveNonStockroomParts);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

               

                if (navigationData != null)
                {
                    this.WorkorderID =Convert.ToInt32(navigationData);
                }



              //  FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();


            }
            catch (Exception ex)
            {
                OperationInProgress = false;

            }

            finally
            {
                OperationInProgress = false;
            }
        }

        public CreateNonStockroomPartsPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

                //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
              
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("CreateNonStockroomParts");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    PartNameTitle = WebControlTitle.GetTargetNameByTitleName("PartName");
                    PartNumberTitle = WebControlTitle.GetTargetNameByTitleName("PartNumber");
                    QuantityRequiredtitle = WebControlTitle.GetTargetNameByTitleName( "QuantityRequired");
                    UnitCosttitle = WebControlTitle.GetTargetNameByTitleName( "UnitCost");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");

              
            }
            catch (Exception ex)
            {


            }

            finally
            {

            }
        }







        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
                }




            }
            catch (Exception ex)
            {
                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }



        public async Task SaveNonStockroomParts()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));


                //OperationInProgress = true;

                

                //Check Mandatory fields and other Validations////
                var partresponse = await _workorderService.GetWorkorderNonStockroomParts(this.WorkorderID,null);

                if (String.IsNullOrEmpty(PartNameText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PartNameismandatoryfield"));
                    return;
                }
                else if (String.IsNullOrEmpty(PartNumberText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PartNumberismandatoryfield"));
                    return;
                }
                ///Check Duplicate Part//////

                else if (partresponse.workOrderWrapper.workOrderNonStockroomParts != null && partresponse.workOrderWrapper.workOrderNonStockroomParts.Any(x => x.PartNumber == PartNumberText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PartNumberisalreadyExist"));
                    return;
                }
                else if (String.IsNullOrEmpty(QuantityRequiredText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("QunatityRequiredismandatoryfield"));
                    return;
                }
                else if (String.IsNullOrEmpty(UnitCostText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("UnitCostismandatoryfield"));
                    return;
                }

                else
                {

                    decimal _QuantityRequired1;
                    if (!decimal.TryParse(QuantityRequiredText, out _QuantityRequired1))
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefillthevalidQuantityRequired"));
                        return;
                    }

                  

                    decimal _UnitCost1;
                    if (!decimal.TryParse(UnitCostText, out _UnitCost1))
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthevalidunitcost"));
                        return;
                    }
                    try
                    {
                       
                        var k = Convert.ToInt32(QuantityRequiredText);
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseenterthevalidquantity"));
                        return;
                    }
                }

                var workOrderNonStockroompart = new WorkOrderNonStockroomParts();
                #region workOrdernonStockroompart properties initialzation


                workOrderNonStockroompart.PartName = String.IsNullOrEmpty(PartNameText.Trim()) ? null : PartNameText.Trim();
                workOrderNonStockroompart.PartNumber = String.IsNullOrEmpty(PartNumberText.Trim()) ? null : PartNumberText.Trim();
                workOrderNonStockroompart.QuantityRequired = int.Parse(QuantityRequiredText);
                workOrderNonStockroompart.UnitCostAmount = decimal.Parse(UnitCostText);
                workOrderNonStockroompart.WorkOrderID = this.WorkorderID;


                #endregion






                #endregion


                var workorder = new workOrderWrapper
                {
                    TimeZone = AppSettings.UserTimeZone,
                    CultureName = AppSettings.UserCultureName,
                    UserId = Convert.ToInt32(UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                   
                    workOrderNonStockroomPart = workOrderNonStockroompart,
                    

                };


                var response = await _workorderService.CreateWorkorderNonStockroomParts(workorder);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("NonStockroomPartisAddedSuccessfully"), 2000);
                    //await NavigationService.NavigateBackAsync();
                    await NavigationService.NavigateBackAsync();


                }
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;



            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }
        }







        public Task OnViewDisappearingAsync(VisualElement view)
        {

            return Task.FromResult(true);

        }

        public Task OnViewAppearingAsync(VisualElement view)
        {
            throw new NotImplementedException();
        }



    }
}
