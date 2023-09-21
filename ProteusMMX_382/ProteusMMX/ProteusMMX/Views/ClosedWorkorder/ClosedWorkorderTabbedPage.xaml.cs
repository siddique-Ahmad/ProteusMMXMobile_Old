using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class ClosedWorkorderTabbedPage : TabbedPage
	{
		public ClosedWorkorderTabbedPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
          
          
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

            this.CurrentPageChanged += (object sender, EventArgs e) => {
                try
                {
                    var index = this.Children.IndexOf(this.CurrentPage);
                    var CurrentPage = this.Children.ElementAt(index);
                    string Selectedpage = CurrentPage.ToString();
                    if (Selectedpage.Contains("ClosedWorkorderDetailsPage"))
                    {
                        this.Title = WebControlTitle.GetTargetNameByTitleName("Details");
                    }
                    else if (Selectedpage.Contains("ClosedWorkorderTaskAndLabourPage"))
                    {
                        this.Title = WebControlTitle.GetTargetNameByTitleName("TasksandLabor");
                    }
                    else if (Selectedpage.Contains("ClosedWorkorderInspection"))
                    {
                        this.Title = WebControlTitle.GetTargetNameByTitleName("Inspection");
                    }
                    else if (Selectedpage.Contains("WorkorderStockroomPartsTabbedPage"))
                    {
                        this.Title = WebControlTitle.GetTargetNameByTitleName("Parts");
                    }
                    else if (Selectedpage.Contains("ClosedWorkorderToolsPage"))
                    {
                        this.Title = WebControlTitle.GetTargetNameByTitleName("Tools");
                    }
                    else if (Selectedpage.Contains("ClosedWorkorderAttachments"))
                    {
                        this.Title = WebControlTitle.GetTargetNameByTitleName("Attachments");
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                    
                }
                

            };
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
                await viewAware.OnViewDisappearingAsync(this);
            }
        }

    }
}