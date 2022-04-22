using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.PurchaseOrderModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.PurchaseOrder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace ProteusMMX.ViewModel.PurchaseOrder
{
    public class ReceivePuchaseOrderAssetViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IPurchaseOrderService _purchaseOrderService;
        #endregion
        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;
                    OnPropertyChanged(nameof(FormControlsAndRights));
                }
            }
        }
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
        bool _isPickerDataRequested;
        public bool IsPickerDataRequested
        {
            get { return _isPickerDataRequested; }
            set
            {
                if (value != _isPickerDataRequested)
                {
                    _isPickerDataRequested = value;
                    OnPropertyChanged(nameof(IsPickerDataRequested));
                }
            }
        }

        bool _isPickerDataSubscribed;
        public bool IsPickerDataSubscribed
        {
            get { return _isPickerDataSubscribed; }
            set
            {
                if (value != _isPickerDataSubscribed)
                {
                    _isPickerDataSubscribed = value;
                    OnPropertyChanged(nameof(IsPickerDataSubscribed));
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
       string _receivedDateTitle;
        public string ReceivedDateTitle
        {
            get
            {
                return _receivedDateTitle;
            }

            set
            {
                if (value != _receivedDateTitle)
                {
                    _receivedDateTitle = value;
                    OnPropertyChanged(nameof(ReceivedDateTitle));
                }
            }
        }
        string _receiverNameTitle;
        public string ReceiverNameTitle
        {
            get
            {
                return _receiverNameTitle;
            }

            set
            {
                if (value != _receiverNameTitle)
                {
                    _receiverNameTitle = value;
                    OnPropertyChanged(nameof(ReceiverNameTitle));
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
        int? _receiverID;
        public int? ReceiverID
        {
            get
            {
                return _receiverID;
            }

            set
            {
                if (value != _receiverID)
                {
                    _receiverID = value;
                    OnPropertyChanged(nameof(ReceiverID));
                }
            }
        }
        int _purchaseOrderAssetID;
        public int PurchaseOrderAssetID
        {
            get
            {
                return _purchaseOrderAssetID;
            }

            set
            {
                if (value != _purchaseOrderAssetID)
                {
                    _purchaseOrderAssetID = value;
                    OnPropertyChanged(nameof(PurchaseOrderAssetID));
                }
            }
        }

        string _receiverName;
        public string ReceiverName
        {
            get
            {
                return _receiverName;
            }

            set
            {
                if (value != _receiverName)
                {
                    _receiverName = value;
                    OnPropertyChanged(nameof(ReceiverName));
                }
            }
        }
        DateTime _receivedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
        public DateTime ReceivedDate
        {
            get
            {
                return _receivedDate;
            }

            set
            {
                if (value != _receivedDate)
                {
                    _receivedDate = value;
                    OnPropertyChanged(nameof(ReceivedDate));
                }
            }
        }
        string _invoiceNumber;
        public string InvoiceNumber
        {
            get
            {
                return _invoiceNumber;
            }

            set
            {
                if (value != _invoiceNumber)
                {
                    _invoiceNumber = value;
                    OnPropertyChanged(nameof(InvoiceNumber));
                }
            }
        }
        string _packagingSlipNumber;
        public string PackagingSlipNumber
        {
            get
            {
                return _packagingSlipNumber;
            }

            set
            {
                if (value != _packagingSlipNumber)
                {
                    _packagingSlipNumber = value;
                    OnPropertyChanged(nameof(PackagingSlipNumber));
                }
            }
        }
        string _invoiceNumberText;
        public string InvoiceNumberText
        {
            get
            {
                return _invoiceNumberText;
            }

            set
            {
                if (value != _invoiceNumberText)
                {
                    _invoiceNumberText = value;
                    OnPropertyChanged(nameof(InvoiceNumberText));
                }
            }
        }
        string _packagingSlipNumberText;
        public string PackagingSlipNumberText
        {
            get
            {
                return _packagingSlipNumberText;
            }

            set
            {
                if (value != _packagingSlipNumberText)
                {
                    _packagingSlipNumberText = value;
                    OnPropertyChanged(nameof(PackagingSlipNumberText));
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

        bool _receivedButtonIsVisible = true;
        public bool ReceivedButtonIsVisible
        {
            get
            {
                return _receivedButtonIsVisible;
            }

            set
            {
                if (value != _receivedButtonIsVisible)
                {
                    _receivedButtonIsVisible = value;
                    OnPropertyChanged(nameof(ReceivedButtonIsVisible));
                }
            }
        }
        Color _colorBackground = Color.FromHex("#87CEFA");
        public Color ColorBackground
        {
            get
            {
                return _colorBackground;
            }

            set
            {
                if (value != _colorBackground)
                {
                    _colorBackground = value;
                    OnPropertyChanged("ColorBackground");
                }
            }
        }
        bool _receivedButtonIsEnabled = true;
        public bool ReceivedButtonIsEnabled
        {
            get
            {
                return _receivedButtonIsEnabled;
            }

            set
            {
                if (value != _receivedButtonIsEnabled)
                {
                    _receivedButtonIsEnabled = value;
                    OnPropertyChanged(nameof(ReceivedButtonIsEnabled));
                }
            }
        }
        
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
        string _receiveTitle = "";
        public string ReceiveTitle
        {
            get
            {
                return _receiveTitle;
            }

            set
            {
                if (value != _receiveTitle)
                {
                    _receiveTitle = value;
                    OnPropertyChanged("ReceiveTitle");
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
        public ICommand ReceivePurchaseOrderAssetCommand => new AsyncCommand(ReceivePurchaseOrderAsset);

        public ICommand ReceiverCommand => new AsyncCommand(ShowReceiver);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

                Application.Current.Properties["ModuleName"] = "PO";


                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    this.PurchaseOrderAssetID = navigationParams.PurchaseOrderAssetID;
                }



               // FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();
                //FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.ReceivingModuleName);
                //if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                //{
                //    var PurchaseOrderModule = FormControlsAndRights.lstModules[0];
                //    if (PurchaseOrderModule.ModuleName == "Purchasing") //ModuleName can't be  changed in service 
                //    {
                //        if (PurchaseOrderModule.lstSubModules != null && PurchaseOrderModule.lstSubModules.Count > 0)
                //        {

                //            var PurchaseOrderSubModule = PurchaseOrderModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "PurchaseOrders");

                //            if (PurchaseOrderSubModule != null)
                //            {
                //                if (PurchaseOrderSubModule.Button != null && PurchaseOrderSubModule.Button.Count > 0)
                //                {
                //                    //  CloseWorkorderRights = workorderSubModule.Button.FirstOrDefault(i => i.Name == "Close");

                //                }

                //                if (PurchaseOrderSubModule.listDialoges != null && PurchaseOrderSubModule.listDialoges.Count > 0)
                //                {
                //                    var PurchaseOrderDialog = PurchaseOrderSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "Receiving");
                //                    if (PurchaseOrderDialog != null && PurchaseOrderDialog.listTab != null && PurchaseOrderDialog.listTab.Count > 0)
                //                    {
                //                        var PONonStockPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "NonStockroomParts");
                //                        var POAssetsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Assets");
                //                        var POPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Parts");
                //                        var POPartsRecieveButton = POPartsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveParts");
                //                        var PONonStockPartsRecieveButton = PONonStockPartsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveNonStockroomParts");
                //                        var POAssetsRecieveButton = POAssetsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveAssets");
                //                        if (POAssetsRecieveButton.Expression == "E")
                //                        {
                //                            this.ReceivedButtonIsVisible = true;
                //                        }
                //                        else if (POAssetsRecieveButton.Expression == "V")
                //                        {
                //                            this.ReceivedButtonIsVisible = true;
                //                            this.ReceivedButtonIsEnabled = false;

                //                        }
                //                        else 
                //                        {
                //                            this.ReceivedButtonIsVisible = false;
                //                        }

                //                    }
                //                }
                //            }
                //        }
                //    }
                //    OperationInProgress = false;
                //}

                if (Application.Current.Properties.ContainsKey("ReceiveAssets"))
                {
                    var ReceiveParts = Application.Current.Properties["ReceiveAssets"].ToString();
                    if (ReceiveParts == "E")
                    {
                        this.ReceivedButtonIsVisible = true;

                    }
                    else if (ReceiveParts == "V")
                    {
                        this.ReceivedButtonIsVisible = true;
                        this.ReceivedButtonIsEnabled = false;
                        this.ColorBackground = Color.FromHex("#d3d3d3");

                    }
                    else
                    {
                        this.ReceivedButtonIsVisible = false;
                      

                    }
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

        public ReceivePuchaseOrderAssetViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IPurchaseOrderService purchaseOrderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

                //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("ReceivingAssetDetails");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    PartNameTitle = WebControlTitle.GetTargetNameByTitleName("PartName");
                    PartNumberTitle = WebControlTitle.GetTargetNameByTitleName("PartNumber");
                    QuantityRequiredtitle = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
                    UnitCosttitle = WebControlTitle.GetTargetNameByTitleName("UnitCost");
                    InvoiceNumber = WebControlTitle.GetTargetNameByTitleName("InvoiceNumber");
                    PackagingSlipNumber = WebControlTitle.GetTargetNameByTitleName("PackagingSlipNumber");
                    ReceivedDateTitle = WebControlTitle.GetTargetNameByTitleName("ReceivedDate");
                    ReceiverNameTitle = WebControlTitle.GetTargetNameByTitleName("Receiver");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    ReceiveTitle= WebControlTitle.GetTargetNameByTitleName("Receive");
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



        public async Task ReceivePurchaseOrderAsset()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

               // OperationInProgress = true;

               
                if(string.IsNullOrWhiteSpace(this.ReceiverName))
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("RecieverismandatoryField"), 2000);

                   // await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("RecieverismandatoryField"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    return;
                }



                if (ReceivedDate == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefillReceiveddate"), 2000);
                    return;
                }

                var POAsset = new PurchaseOrderAssetsReceiving
                {
                    UserId = Convert.ToInt32(UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    InvoiceNumber = String.IsNullOrEmpty(InvoiceNumberText) ? null : InvoiceNumberText.Trim(),
                    PurchaseOrderAssetID = this.PurchaseOrderAssetID,
                    ReceivedDate = ReceivedDate.Date.Add(DateTime.Now.TimeOfDay),
                    PackingSlipNumber = String.IsNullOrEmpty(PackagingSlipNumberText) ? null : PackagingSlipNumberText.Trim(),
                    ReceiverID = this.ReceiverID,
                    ModifiedUserName = AppSettings.User.UserName,


                };


                var response = await _purchaseOrderService.ReceivePurchaseOrderAssets(POAsset);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Assetisrecievedsuccessfully"), 2000);
                    await  NavigationService.NavigateBackAsync();

                }
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;



            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;

            }
        }




        public async Task ShowReceiver()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

               // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<ReceiverListSelectionPageViewModel>(); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

              //  OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;

            }
        }
        private void OnReceiverRequested(object obj)
        {

            if (obj != null)
            {
                var receiver = obj as POReceiver;
                this.ReceiverID = receiver.ReceiverID;
                this.ReceiverName = receiver.ReceiverName;



            }


        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {

            return Task.FromResult(true);

        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                OperationInProgress = true;

                if (!IsPickerDataSubscribed)
                {
                    //Retrive Receivers
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.ReceiverRequested, OnReceiverRequested);

                   

                    IsPickerDataSubscribed = true;
                }

                else if (IsPickerDataRequested)
                {

                    IsPickerDataRequested = false;
                    return;
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



    }
}

#endregion