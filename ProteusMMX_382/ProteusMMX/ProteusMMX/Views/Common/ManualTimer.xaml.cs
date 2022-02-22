using ProteusMMX.Model.CommonModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualTimer : PopupPage
    {
        public ManualTimer(object navigationData)
        {
            InitializeComponent();
            if (navigationData != null)
            {
                var navigationParams = navigationData as TargetNavigationData;
                //  ViewModel = navigationParams.ViewModel;

                TaskID.Text = navigationParams.TaskID.ToString();
                WorkOrderLabourId.Text = navigationParams.WorkOrderLabourId.ToString();
                HrsText.Text = navigationParams.HrsText.ToString();
            }

        }

        private void CancelBtn_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
            // PopupNavigation.PopAsync();
        }
        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                Application.Current.Properties["TaskIDkey"] = TaskID.Text;
                Application.Current.Properties["WorkOrderLabourIdkey"] = WorkOrderLabourId.Text;
                Application.Current.Properties["HrsTextkey"] = HrsText.Text;
                string hours = Hrs.Text;
                string minutes = mint.Text;
                if (String.IsNullOrEmpty(hours))
                {
                    hours = "0";
                }
                if (String.IsNullOrEmpty(minutes))
                {
                    minutes = "0";
                }
                decimal decHour1 = decimal.Parse(hours + "." + minutes);

                Application.Current.Properties["decHourKey"] = decHour1;
                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        void Hrs_TextChanged(object sender, EventArgs e)
        {
            //btnComp.IsEnabled = true;
            //btnComp.BackgroundColor = Color.FromHex("#87CEFA");

            //btnStart.IsEnabled = false;
            //btnStart.BackgroundColor = Color.FromHex("#D3D3D3");
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }

        }

        async void mint_TextChanged(object sender, EventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }

            decimal minuteValue;
            if (string.IsNullOrWhiteSpace(val))
            {
                return;
            }
            var x = decimal.TryParse(val, out minuteValue);
            if (!x)
            {
                e1.Text = "";
                return;
            }
            if (minuteValue > 59)
            {
                e1.Text = "";
                return;
            }

        }

    }
}