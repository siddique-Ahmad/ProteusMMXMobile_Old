using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Model;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Inventory;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.Attachments;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.Barcode
{
    public class SearchAssetForAttachmentViewModel:ViewModelBase
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IAttachmentService _attachmentService;
        protected readonly IAssetModuleService _assetService;

        #endregion

        #region Properties
        int? _closedWorkorderID;
        public int? ClosedWorkorderID
        {
            get
            {
                return _closedWorkorderID;
            }

            set
            {
                if (value != _closedWorkorderID)
                {
                    _closedWorkorderID = value;
                    OnPropertyChanged(nameof(ClosedWorkorderID));
                }
            }
        }
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

        #endregion

        #region Title Properties

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

        string _searchPlaceholder;
        public string SearchPlaceholder
        {
            get
            {
                return _searchPlaceholder;
            }

            set
            {
                if (value != _searchPlaceholder)
                {
                    _searchPlaceholder = value;
                    OnPropertyChanged("SearchPlaceholder");
                }
            }
        }

        string _goTitle;
        public string GoTitle
        {
            get
            {
                return _goTitle;
            }

            set
            {
                if (value != _goTitle)
                {
                    _goTitle = value;
                    OnPropertyChanged("GoTitle");
                }
            }
        }

        string _scanTitle;
        public string ScanTitle
        {
            get
            {
                return _scanTitle;
            }

            set
            {
                if (value != _scanTitle)
                {
                    _scanTitle = value;
                    OnPropertyChanged("ScanTitle");
                }
            }
        }

        string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    OnPropertyChanged("SearchText");
                    SearchText_TextChanged();
                }
            }
        }

        string _searchButtonTitle;
        public string SearchButtonTitle
        {
            get
            {
                return _searchButtonTitle;
            }

            set
            {
                if (value != _searchButtonTitle)
                {
                    _searchButtonTitle = value;
                    OnPropertyChanged("SearchButtonTitle");
                }
            }
        }
        string _selectOptionsTitle;
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
        string _swipeText;
        public string SwipeText
        {
            get
            {
                return _swipeText;
            }

            set
            {
                if (value != _swipeText)
                {
                    _swipeText = value;
                    OnPropertyChanged(nameof(SwipeText));
                }
            }
        }


        #endregion



        #endregion


        #region Attachment Page Properties
        string _imageText;
        public string ImageText
        {
            get
            {
                return _imageText;
            }

            set
            {
                if (value != _imageText)
                {
                    _imageText = value;
                    OnPropertyChanged(nameof(ImageText));
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



        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }


        bool? _inspectionUser = AppSettings.User.IsInspectionUser;
        public bool? InspectionUser
        {
            get { return _inspectionUser; }
        }




        bool _isDataRequested;
        public bool IsDataRequested
        {
            get { return _isDataRequested; }
            set
            {
                if (value != _isDataRequested)
                {
                    _isDataRequested = value;
                    OnPropertyChanged(nameof(IsDataRequested));
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




        public ObservableCollection<WorkorderAttachment> Attachments { get; set; }
        public ObservableCollection<string> DocumentAttachments { get; set; }

        int _selectedIndexItem = -1;
        public int SelectedIndexItem
        {
            get
            {
                return _selectedIndexItem;
            }

            set
            {
                _selectedIndexItem = value;
                OnPropertyChanged("SelectedIndexItem");

            }
        }

        int _selectedPosition;
        public int SelectedPosition
        {
            get
            {
                return _selectedPosition;
            }

            set
            {
                if (value != _selectedPosition)
                {
                    _selectedPosition = value;
                    OnPropertyChanged(nameof(SelectedPosition));
                }
            }
        }

        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand DocCommand => new AsyncCommand(OpenDoc);
        public ICommand CameraCommand => new AsyncCommand(OpenMedia);

        public ICommand ScanCommand => new AsyncCommand(SearchAssetAttachment);

        public bool IsAutoAnimationRunning { get; set; }

        public bool IsUserInteractionRunning { get; set; }

        public ICommand PanPositionChangedCommand { get; }


        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                


                }

                //FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
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

        public SearchAssetForAttachmentViewModel(IAuthenticationService authenticationService, IAttachmentService attachmentService, IAssetModuleService assetService)
        {
            _authenticationService = authenticationService;
            _attachmentService = attachmentService;
            _assetService = assetService;

            Attachments = new ObservableCollection<WorkorderAttachment>
            {

            };

            DocumentAttachments = new ObservableCollection<string>
            {

            };
            PanPositionChangedCommand = new Command(v =>
            {
                if (IsAutoAnimationRunning || IsUserInteractionRunning)
                {
                    return;
                }

                var index = SelectedIndexItem + (bool.Parse(v.ToString()) ? 1 : -1);
                if (index < 0 || index >= Attachments.Count)
                {
                    return;
                }
                SelectedIndexItem = index;
            });
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

                //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                //if (titles != null && titles.listWebControlTitles != null && titles.listWebControlTitles.Count > 0)
                {
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("Attachments");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
                    GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                    ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                    SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    SwipeText = WebControlTitle.GetTargetNameByTitleName("Pleaseswipelefttoright");

                    ImageText = WebControlTitle.GetTargetNameByTitleName("Total") + " " + WebControlTitle.GetTargetNameByTitleName("Image") + " : " + 0;




                }
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
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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
        public async Task OpenDoc()
        {

            try
            {

               // UserDialogs.Instance.ShowLoading();
                OperationInProgress = true;
                var action = await DialogService.SelectActionAsync(WebControlTitle.GetTargetNameByTitleName("ChooseFile"), SelectTitle, CancelTitle, DocumentAttachments.ToArray());
                if (DocumentAttachments.Count == 0)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThereisnoPDFattached"), 2000);
                    return;
                }
                if (action == WebControlTitle.GetTargetNameByTitleName("Cancel") || action == null)
                {
                    UserDialogs.Instance.HideLoading();
                    return;
                }
                  
                DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + action);

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                 OperationInProgress = false;
            }
        }

        public async Task OpenMedia()
        {
            try
            {
                List<string> choice = new List<string>() { WebControlTitle.GetTargetNameByTitleName("Camera"), WebControlTitle.GetTargetNameByTitleName("Gallery") };
                var selected = await DialogService.SelectActionAsync(WebControlTitle.GetTargetNameByTitleName("ChooseFile"), SelectTitle, CancelTitle, choice.ToArray());
                if (selected != null && !selected.Equals(WebControlTitle.GetTargetNameByTitleName("Cancel")))
                {

                    if (selected.Equals(WebControlTitle.GetTargetNameByTitleName("Camera")))
                    {
                        IsDataRequested = true;
                        await TakePhoto();
                    }
                    else
                    {
                        IsDataRequested = true;
                        await PickPhoto();
                    }
                }
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

        public async Task TakePhoto()
        {
            try
            {
               // UserDialogs.Instance.ShowLoading();

                OperationInProgress = true;

                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("No camera available.", 2000);
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "ProteusMMX",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = CameraDevice.Front
                });

                if (file == null)
                {
                    UserDialogs.Instance.HideLoading();

                    return;
                }


                string filepath = file.AlbumPath;
                string filename = "Image" + DateTime.Now.Ticks + ".png";

                byte[] byteImg = StreamToBase64.FileToByte(file);

                Attachments.Add(new WorkorderAttachment
                {
                    IsSynced = false,
                    attachmentFileExtension = "Image" + DateTime.Now.Ticks + ".png",
                    ImageBytes = byteImg,
                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImg)))),

                });


                file.Dispose();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();


                OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

               OperationInProgress = false;
            }

        }

        public async Task PickPhoto()
        {
            try
            {
               // UserDialogs.Instance.ShowLoading();

                OperationInProgress = true;
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("No camera available.", 2000);
                    return;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                {
                    UserDialogs.Instance.HideLoading();

                    return;
                }


                string filepath = file.AlbumPath;
                string filename = "Image" + DateTime.Now.Ticks + ".png";

                byte[] byteImg = StreamToBase64.FileToByte(file);

                Attachments.Add(new WorkorderAttachment
                {
                    IsSynced = false,
                    attachmentFileExtension = "Image" + DateTime.Now.Ticks + ".png",
                    ImageBytes = byteImg,
                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImg)))),

                });

                file.Dispose();
            }
            catch (Exception)
            {
                UserDialogs.Instance.HideLoading();

               OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

               OperationInProgress = false;
            }
        }





        public async Task OnViewAppearingAsync(VisualElement view)
        {
       
        }
        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }
        public async Task SearchAssetAttachment()
        {

            try
            {
                OperationInProgress = true;


                #region Barcode Section and Search Section

                if (SearchButtonTitle == ScanTitle)
                {
                    var options = new MobileBarcodeScanningOptions()
                    {
                        AutoRotate = false,
                        TryHarder = true,

                    };

                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };

                    _scanner.OnScanResult += _scanner_OnScanResult;
                    var navPage = App.Current.MainPage as NavigationPage;
                    await navPage.PushAsync(_scanner);
                }

                else
                {
                    //reset pageno. and start search again.
                    await GetAssetAttachment();

                }

                #endregion

                //await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();
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


        private async void _scanner_OnScanResult(ZXing.Result result)
        {
            //Set the text property
            this.SearchText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                //reset pageno. and start search again.
                await GetAssetAttachment();


            });

        }
        async Task GetAttachmentByAsset()
        {
            try
            {
                OperationInProgress = true;


                try
                {


                    #region Remove all existing image and documents from ViewModel
                    if (Attachments != null && Attachments.Count > 0)
                    {
                        var count = Attachments.Count;
                        for (int i = 1; i <= count; i++)
                        {
                            Attachments.RemoveAt(count - i);
                        }

                    }

                    if (DocumentAttachments != null && DocumentAttachments.Count > 0)
                    {
                        var count = DocumentAttachments.Count;
                        for (int i = 1; i <= count; i++)
                        {
                            DocumentAttachments.RemoveAt(count - i);
                        }

                    }
                    #endregion

                    //var AssetResponse = await _assetService.GetAssetsFromSearchBar(this.SearchText, UserID);
                    //if (AssetResponse.assetWrapper.assets == null || AssetResponse.assetWrapper.assets.Count == 0)
                    //{
                    //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisAssetdoesnotExist"), 2000);
                    //    return;
                    //}

                    var attachment = await _attachmentService.GetAssetAttachmentsByAssetNumber(this.SearchText.ToString());

                    if (attachment.AssetExist == false)
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisAssetdoesnotExist"), 2000);
                        return;
                    }
                    if (attachment.assetWrapper.attachments == null || attachment.assetWrapper.attachments.Count == 0)
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("NoAttachmentfoundforthisAsset"), 2000);
                        return;
                    }
                  
                    if (attachment.assetWrapper != null && attachment.assetWrapper.attachments != null)
                    {

                        if (attachment.assetWrapper.attachments.Count > 0)
                        {
                            ImageText = WebControlTitle.GetTargetNameByTitleName("Total") + " " + WebControlTitle.GetTargetNameByTitleName("Image") + " : " + attachment.assetWrapper.attachments.Count;
                            foreach (var file in attachment.assetWrapper.attachments)
                            {

                                if (file.AttachmentNameWithExtension != null &&
                                    (file.AttachmentNameWithExtension.Contains(".pdf") ||
                                    file.AttachmentNameWithExtension.Contains(".doc") ||
                                    file.AttachmentNameWithExtension.Contains(".docx") ||
                                    file.AttachmentNameWithExtension.Contains(".xls") ||
                                    file.AttachmentNameWithExtension.Contains(".xlsx") ||
                                    file.AttachmentNameWithExtension.Contains(".txt")))
                                {

                                    DocumentAttachments.Add(file.AttachmentNameWithExtension);
                                }
                                else
                                {
                                    byte[] imgUser = StreamToBase64.StringToByte(file.Attachment);
                                    MemoryStream stream = new MemoryStream(imgUser);
                                    bool isimage = Extension.IsImage(stream);
                                    if (isimage == true)
                                    {

                                        //byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 350, 350);


                                        Attachments.Add(new WorkorderAttachment
                                        {
                                            IsSynced = true,
                                            attachmentFileExtension = file.AttachmentNameWithExtension,
                                            ImageBytes = imgUser, //byteImage,
                                                                  // WorkOrderAttachmentID = file.WorkOrderAttachmentID,
                                                                  // AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImage)))),
                                            AttachmentImageSource = ImageSource.FromStream(() => new MemoryStream(imgUser))
                                        }
                                        );
                                    }


                                }


                            }

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
            catch (Exception ex)
            {
                OperationInProgress = false;
            }
            finally
            {
                OperationInProgress = false;
            }
        }
        private async Task GetAssetAttachment()
        {

            await GetAttachmentByAsset();

        }

        private void SearchText_TextChanged()
        {

            try
            {
                if (SearchText == null || SearchText.Length == 0)
                {
                    SearchButtonTitle = ScanTitle;
                }

                else if (SearchText.Length > 0)
                {
                    SearchButtonTitle = GoTitle;
                }
            }
            catch (Exception ex)
            {


            }
        }

        #endregion
    }
    public class WorkorderAttachment
    {
        public bool IsSynced { get; set; }
        public byte[] ImageBytes { get; set; }
        public string attachmentFileExtension { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public ImageSource AttachmentImageSource { get; set; }
        public int? WorkOrderAttachmentID { get; set; }
    }
}
