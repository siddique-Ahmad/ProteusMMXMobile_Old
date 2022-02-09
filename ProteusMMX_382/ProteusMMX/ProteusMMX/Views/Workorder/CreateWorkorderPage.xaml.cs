using ProteusMMX.DependencyInterface;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
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
    public partial class CreateWorkorderPage : ContentPage
    {
        public CreateWorkorderPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public CreateWorkorderPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as CreateWorkorderPageViewModel;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                await viewAware.OnViewDisappearingAsync(this);
            }
        }

        private void NumericEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }


            string requred = e1.FontFamily;
            if (requred == "true")
            {
                var BorderColor = e1.Parent as SfBorder;
                if (string.IsNullOrEmpty(e1.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void NumericEntry_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            string requred = e1.FontFamily;
            if (requred == "true")
            {
                var BorderColor = e1.Parent as SfBorder;
                if (string.IsNullOrEmpty(e1.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void NumericEntry_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            string requred = e1.FontFamily;
            if (requred == "true")
            {
                var BorderColor = e1.Parent as SfBorder;
                if (string.IsNullOrEmpty(e1.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void NumericEntry_TextChanged_3(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            string requred = e1.FontFamily;
            if (requred == "true")
            {
                var BorderColor = e1.Parent as SfBorder;
                if (string.IsNullOrEmpty(e1.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }
        private void RadioButton_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (RadioButton.IsChecked == false)
            {
                return;
            }
            else
            {
                Button1.IsChecked = false;
            }

        }
        private void Button1_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (Button1.IsChecked == false)
            {
                return;
            }
            else
            {
                RadioButton.IsChecked = false;
            }

        }

        private void Description_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Editor Desc = sender as Editor;
            string requred = Desc.FontFamily;
            if (requred == "true")
            {
                var BorderColor = Desc.Parent as SfBorder;
                if (string.IsNullOrEmpty(Desc.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }

        }

        private void AssignedToEmployee_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry AssignedToEmployee = sender as Entry;
            string requred = AssignedToEmployee.FontFamily;
            if (requred == "true")
            {
                var BorderColor = AssignedToEmployee.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(AssignedToEmployee.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void Facility_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry Cause = sender as Entry;

            if (string.IsNullOrEmpty(Cause.Text))
            {
                var BorderColor = Cause.Parent.Parent as SfBorder;
                BorderColor.BorderColor = Color.Red;
            }
            else
            {
                var BorderColor = Cause.Parent.Parent as SfBorder;
                BorderColor.BorderColor = Color.Black;
            }
        }

        private void Location_PropertyChanged(object sender, TextChangedEventArgs e)
        {

            var Location = sender as Entry;
            var LocationName = LocationNameBtn as Entry;
            var AssetName = AssetNameBtn as Entry;
            var AssignToEmployee = AssetSystemNameBtn as Entry;

            var LocationNamePar = LocationNameBtn.Parent.Parent as SfBorder;
            var AssetNamePar = AssetNameBtn.Parent.Parent as SfBorder;
            var AssignToEmployeePar = AssetSystemNameBtn.Parent.Parent as SfBorder;


            if (!string.IsNullOrEmpty(Location.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else if (!string.IsNullOrEmpty(AssetName.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else if (!string.IsNullOrEmpty(AssignToEmployee.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else
            {
                LocationNamePar.BorderColor = Color.Red;
                AssetNamePar.BorderColor = Color.Red;
                AssignToEmployeePar.BorderColor = Color.Red;
            }


        }

        private  void  Asset_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            var Asset = sender as Entry;
            var LocationName = LocationNameBtn as Entry;
            var AssetName = AssetNameBtn as Entry;
            var AssignToEmployee = AssetSystemNameBtn as Entry;

            var LocationNamePar = LocationNameBtn.Parent.Parent as SfBorder;
            var AssetNamePar = AssetNameBtn.Parent.Parent as SfBorder;
            var AssignToEmployeePar = AssetSystemNameBtn.Parent.Parent as SfBorder;


            if (!string.IsNullOrEmpty(Asset.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else if (!string.IsNullOrEmpty(LocationName.Text))
            {


                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else if (!string.IsNullOrEmpty(AssignToEmployee.Text))
            {

                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else
            {

                LocationNamePar.BorderColor = Color.Red;
                AssetNamePar.BorderColor = Color.Red;
                AssignToEmployeePar.BorderColor = Color.Red;
            }

        }

        private void AssetSystem_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            var AssetSystem = sender as Entry;
            var LocationName = LocationNameBtn as Entry;
            var AssetName = AssetNameBtn as Entry;
            var AssignToEmployee = AssetSystemNameBtn as Entry;

            var LocationNamePar = LocationNameBtn.Parent.Parent as SfBorder;
            var AssetNamePar = AssetNameBtn.Parent.Parent as SfBorder;
            var AssignToEmployeePar = AssetSystemNameBtn.Parent.Parent as SfBorder;


            if (!string.IsNullOrEmpty(AssetSystem.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else if (!string.IsNullOrEmpty(LocationName.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else if (!string.IsNullOrEmpty(AssignToEmployee.Text))
            {
                LocationNamePar.BorderColor = Color.Black;
                AssetNamePar.BorderColor = Color.Black;
                AssignToEmployeePar.BorderColor = Color.Black;
            }
            else
            {
                LocationNamePar.BorderColor = Color.Red;
                AssetNamePar.BorderColor = Color.Red;
                AssignToEmployeePar.BorderColor = Color.Red;
            }
        }

        private void WorkOrderRequester_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry WorkType = sender as Entry;
            string requred = WorkType.FontFamily;
            if (requred == "true")
            {
                var BorderColor = WorkType.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(WorkType.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void CostCenter_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry WorkType = sender as Entry;
            string requred = WorkType.FontFamily;
            if (requred == "true")
            {
                var BorderColor = WorkType.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(WorkType.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void Priority_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry WorkType = sender as Entry;
            string requred = WorkType.FontFamily;
            if (requred == "true")
            {
                var BorderColor = WorkType.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(WorkType.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void Shift_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry WorkType = sender as Entry;
            string requred = WorkType.FontFamily;
            if (requred == "true")
            {
                var BorderColor = WorkType.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(WorkType.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void WorkorderType_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry WorkType = sender as Entry;
            string requred = WorkType.FontFamily;
            if (requred == "true")
            {
                var BorderColor = WorkType.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(WorkType.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void workorderStatus_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry WorkTypeStatus = sender as Entry;
            string requred = WorkTypeStatus.FontFamily;
            if (requred == "true")
            {
                var BorderColor = WorkTypeStatus.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(WorkTypeStatus.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void Cause_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry Cause = sender as Entry;
            string requred = Cause.FontFamily;
            if (requred == "true")
            {
                var BorderColor = Cause.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(Cause.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void MaintenanceCode_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            Entry MaintenanceCode = sender as Entry;
            string requred = MaintenanceCode.FontFamily;
            if (requred == "true")
            {
                var BorderColor = MaintenanceCode.Parent.Parent as SfBorder;
                if (string.IsNullOrEmpty(MaintenanceCode.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }

        private void CurrentRuntime_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            string requred = e1.FontFamily;
            if (requred == "true")
            {
                var BorderColor = e1.Parent as SfBorder;
                if (string.IsNullOrEmpty(e1.Text))
                {
                    BorderColor.BorderColor = Color.Red;
                }
                else
                {
                    BorderColor.BorderColor = Color.Black;
                }
            }
        }
    }
}