using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Helpers.Validation;
using ProteusMMX.Model;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.Attachments;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class ClosedWorkorderAttachmentsViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IAttachmentService _attachmentService;

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


        #region Attachment Page Properties


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

                    var workorder = navigationParams.Parameter as ClosedWorkOrder;
                    this.ClosedWorkorderID = workorder.ClosedWorkOrderID;


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

        public ClosedWorkorderAttachmentsViewModel(IAuthenticationService authenticationService, IAttachmentService attachmentService)
        {
            _authenticationService = authenticationService;
            _attachmentService = attachmentService;

            Attachments = new ObservableCollection<WorkorderAttachment>
            {

            };

            DocumentAttachments = new ObservableCollection<string>
            {

            };

        }

        public async Task 
            SetTitlesPropertiesForPage()
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
                    //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 
                    SwipeText = WebControlTitle.GetTargetNameByTitleName("Pleaseswipelefttoright");
                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");








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
              //  UserDialogs.Instance.ShowLoading();

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
               // UserDialogs.Instance.HideLoading();

                 OperationInProgress = false;
            }

            finally
            {
               // UserDialogs.Instance.HideLoading();

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

                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }

        }

        public async Task TakePhoto()
        {
            try
            {
                //UserDialogs.Instance.ShowLoading();

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
                //UserDialogs.Instance.HideLoading();

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

                OperationInProgress = true;
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    DialogService.ShowToast("No camera available.", 2000);
                    return;
                }

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                {
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
                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }



      
     
        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {
                OperationInProgress = true;

                if (IsDataRequested)
                {
                    IsDataRequested = false;
                    return;
                }

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

                    //if (ConnectivityService.IsConnected == false)
                    //{
                    //    DialogService.ShowToast("internet not available", 2000);
                    //    return;

                    //}

                    var attachment = await _attachmentService.ClosedWorkorderAttachmentByClosedWorkorderID(this.ClosedWorkorderID.ToString());


                    if (attachment.clWorkOrderWrapper != null && attachment.clWorkOrderWrapper.clattachments != null)
                    {

                        if (attachment.clWorkOrderWrapper.clattachments.Count > 0)
                        {
                            foreach (var file in attachment.clWorkOrderWrapper.clattachments)
                            {

                                if (file.attachmentFileExtension != null &&
                                    (file.attachmentFileExtension.Contains(".pdf") ||
                                    file.attachmentFileExtension.Contains(".doc") ||
                                    file.attachmentFileExtension.Contains(".docx") ||
                                    file.attachmentFileExtension.Contains(".xls") ||
                                    file.attachmentFileExtension.Contains(".xlsx") ||
                                    file.attachmentFileExtension.Contains(".txt")))
                                {

                                    DocumentAttachments.Add(file.attachmentFileExtension);
                                }
                                else
                                {
                                    byte[] imgUser = StreamToBase64.StringToByte(file.attachmentFile);
                                    MemoryStream stream = new MemoryStream(imgUser);
                                    bool isimage = Extension.IsImage(stream);
                                    if (isimage == true)
                                    {

                                        //byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 350, 350);


                                        Attachments.Add(new WorkorderAttachment
                                        {
                                            IsSynced = true,
                                            attachmentFileExtension = file.attachmentFileExtension,
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
        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
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
