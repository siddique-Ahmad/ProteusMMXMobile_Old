﻿using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTaskCustomDatePicker : ContentView
	{
        #region Selected Date Bindable Property

        public static readonly BindableProperty SelectedDateProperty =
           BindableProperty.Create("SelectedDate", typeof(DateTime?), typeof(CustomDatePicker), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: SelectedDatePropertyChanged);

        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        private static void SelectedDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        #endregion

        #region Minimum Date Bindable Property
        public static readonly BindableProperty MinimumDateProperty =
           BindableProperty.Create("MinimumDate", typeof(DateTime?), typeof(CustomDatePicker), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: MinimumDatePropertyChanged);

        public DateTime? MinimumDate
        {
            get { return (DateTime?)GetValue(MinimumDateProperty); }
            set { SetValue(MinimumDateProperty, value); }
        }

        private static void MinimumDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        #endregion

        #region Maximum Date Bindable Property
        public static readonly BindableProperty MaximumDateProperty =
           BindableProperty.Create("MaximumDate", typeof(DateTime?), typeof(CustomDatePicker), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: MaximumDatePropertyChanged);

        public DateTime? MaximumDate
        {
            get { return (DateTime?)GetValue(MaximumDateProperty); }
            set { SetValue(MaximumDateProperty, value); }
        }

        private static void MaximumDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        #endregion

        #region Warning Text Bindable Property
        public static readonly BindableProperty WarningTextProperty =
           BindableProperty.Create("WarningText", typeof(string), typeof(CustomDatePicker), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: WarningTextPropertyChanged);

        public string WarningText
        {
            get { return (string)GetValue(WarningTextProperty); }
            set { SetValue(WarningTextProperty, value); }
        }

        private static void WarningTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

        }
        #endregion
        public AddTaskCustomDatePicker ()
		{
			InitializeComponent ();
		}
        private async void SelectDate_Clicked(object sender, EventArgs e)
        {
            try
            {

                string AutoFillCompleteOnTaskAndLabor = string.Empty;
                string IsHoursRequiredForCompletionDate = string.Empty;

                var dateConfig = new Acr.UserDialogs.DatePromptConfig();
                var timeConfig = new Acr.UserDialogs.TimePromptConfig();
                dateConfig.MinimumDate = this.MinimumDate;
                dateConfig.MaximumDate = this.MaximumDate;
                dateConfig.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                var times = Helpers.DateTime.DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                timeConfig.SelectedTime = times.TimeOfDay;
                dateConfig.UnspecifiedDateTimeKindReplacement = DateTimeKind.Utc;

                if (Device.RuntimePlatform == Device.iOS)
                {
                    dateConfig.iOSPickerStyle = iOSPickerStyle.Wheels;
                    timeConfig.iOSPickerStyle = iOSPickerStyle.Wheels;
                }

                var dateResult = await UserDialogs.Instance.DatePromptAsync(dateConfig);

                var TimeResult = await UserDialogs.Instance.TimePromptAsync(timeConfig);


                if (dateResult.SelectedDate != null && dateResult.SelectedDate.Year == 0001)
                {
                    SelectedDate = null;
                }

                if (dateResult.Ok == false)
                {
                    return;
                }

                if (dateResult.Ok == true)
                {

                    #region Check Hours Required Completion Add Task

                    if (Application.Current.Properties.ContainsKey("AutoFillCompleteOnTaskAndLabor"))
                    {
                        AutoFillCompleteOnTaskAndLabor = Application.Current.Properties["AutoFillCompleteOnTaskAndLabor"].ToString();
                    }
                   
                    if (Application.Current.Properties.ContainsKey("IsHoursRequiredForCompletionDate"))
                    {
                        IsHoursRequiredForCompletionDate = Application.Current.Properties["IsHoursRequiredForCompletionDate"].ToString();
                    }
                    if (!string.IsNullOrWhiteSpace(AutoFillCompleteOnTaskAndLabor))
                    {
                        if (AutoFillCompleteOnTaskAndLabor == "True" && IsHoursRequiredForCompletionDate == "True")
                        {

                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("PleasefillTechnicianHoursForInspection"));
                            return;

                        }
                    }

                    #endregion

                    if (dateResult.SelectedDate != null && dateResult.SelectedDate.Year != 0001)
                    {
                        SelectedDate = dateResult.SelectedDate;
                    }
                    if (SelectedDate != null)
                    {
                        SelectedDate = dateResult.SelectedDate;
                    }
                    if (MinimumDate != null)
                    {

                        if (SelectedDate != null)
                        {
                            SelectedDate = dateResult.SelectedDate;
                        }
                        else
                        {
                            SelectedDate = dateConfig.MinimumDate;
                        }

                    }
                    if (MaximumDate != null)
                    {
                        if (SelectedDate != null)
                        {
                            SelectedDate = dateResult.SelectedDate;
                        }
                        else
                        {
                            SelectedDate = dateConfig.MaximumDate;
                        }

                    }
                    if (MaximumDate == null && SelectedDate == null && MinimumDate == null && dateResult.SelectedDate.Year == 0001)
                    {
                        SelectedDate = DateTime.Now;
                    }

                }

                if (TimeResult.Ok == true)
                {
                    DateTime datetime4 = SelectedDate.Value.Date;
                    DateTime datetime3 = datetime4.Add(TimeResult.SelectedTime);
                    SelectedDate = datetime3;
                }
                //   SelectedDate = dateResult.SelectedDate;


            }
            catch (Exception ex)
            {

            }



        }

        private void ClearDate_Clicked(object sender, EventArgs e)
        {
            SelectedDate = null;
        }
    }
}