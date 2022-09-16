using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class TaskAndLabourPage : ContentPage
	{
        bool _disabledTextIsEnable = false;
        public bool DisabledTextIsEnable
        {
            get
            {
                return _disabledTextIsEnable;
            }

            set
            {
                if (value != _disabledTextIsEnable)
                {
                    _disabledTextIsEnable = value;
                    OnPropertyChanged(nameof(DisabledTextIsEnable));
                }
            }
        }
        bool _taskLabourSearchBoxIsEnable = true;
        public bool TaskLabourSearchBoxIsEnable
        {
            get
            {
                return _taskLabourSearchBoxIsEnable;
            }

            set
            {
                if (value != _taskLabourSearchBoxIsEnable)
                {
                    _taskLabourSearchBoxIsEnable = value;
                    OnPropertyChanged(nameof(TaskLabourSearchBoxIsEnable));
                }
            }
        }
        bool _taskLabourSearchButtonIsEnable = true;
        public bool TaskLabourSearchButtonIsEnable
        {
            get
            {
                return _taskLabourSearchButtonIsEnable;
            }

            set
            {
                if (value != _taskLabourSearchButtonIsEnable)
                {
                    _taskLabourSearchButtonIsEnable = value;
                    OnPropertyChanged(nameof(TaskLabourSearchButtonIsEnable));
                }
            }
        }
        string _disabledText = "";
        public string DisabledText
        {
            get
            {
                return _disabledText;
            }

            set
            {
                if (value != _disabledText)
                {
                    _disabledText = value;
                    OnPropertyChanged("DisabledText");
                }
            }
        }

        public TaskAndLabourPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        TaskAndLabourPageViewModel ViewModel => this.BindingContext as TaskAndLabourPageViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            test1.Children.Clear();
            ServiceOutput InspectionList = await ViewModel._inspectionService.GetWorkorderInspection(ViewModel.WorkorderID.ToString(),AppSettings.User.UserID.ToString());
            if (InspectionList.listInspection != null && InspectionList.listInspection.Count > 0)
            {
               // ViewModel.DisabledText=
                ViewModel.DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                ViewModel.DisabledTextIsEnable = true;
                ViewModel.TaskLabourSearchBoxIsEnable = false;
                ViewModel.TaskLabourSearchButtonIsEnable = false;
                //this.testLabel.Text = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                // _gridViewIsEnable = false;
                // this._gridViewIsEnable.IsVisible = false;
                //this.testLabel.Text = "page disable";
                // this.testLabel.IsVisible = true;

                //this.controlGrid
                //DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                //DisabledTextIsEnable = true;
                //TaskLabourSearchBoxIsEnable = false;
                //TaskLabourSearchButtonIsEnable = false;
                //var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });
                //if (response == LogoutTitle)
                //{
                //  await _authenticationService.LogoutAsync();
                //  await NavigationService.NavigateToAsync<LoginPageViewModel>();
                //  await NavigationService.RemoveBackStackAsync();
                //}

                return;

            }

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        private async void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                //count = count + 1;
                //if (count == 1)
                //{
                    await ViewModel.OnViewDisappearingAsync(null);
                    await ViewModel.GenerateTaskAndLabourLayout();
                //}
                //else
                //{
                //    count = 0;
                //}

            }
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            test1.Children.Clear();
            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
    }
}