using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ProteusMMX.Controls
{
    public partial class CustomCheckBox : ContentView
    {
        public CustomCheckBox()
        {
            InitializeComponent();
        }



        public bool IsChecked
        {
            get
            {
                return (bool)GetValue(IsCheckedProperty);
            }

            set
            {
                SetValue(IsCheckedProperty, value);

              
            }
        }


        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create("IsChecked", typeof(bool), typeof(CustomCheckBox), false, BindingMode.TwoWay);

        //bool _checked;
        //public bool IsChecked
        //{
        //    get
        //    {
        //        return _checked;
        //    }

        //    set
        //    {
        //        if (value != _checked)
        //        {
        //            _checked = value;
        //            OnPropertyChanged("Checked");
        //        }
        //    }
        //}


        //public bool CheckBoxTap
        //{
        //    set
        //    {
        //        if (this.Checked)
        //        {
        //            CheckedImageTapped(null, null);
        //        }
        //        else
        //        {
        //            UncheckedImageTapped(null, null);
        //        }
        //    }
        //}

        public void CheckedImageTapped(object sender, EventArgs args)
        {
            this.CheckedImage.IsVisible = false;
            this.UncheckedImage.IsVisible = true;
            IsChecked = false;

        }

        public void UncheckedImageTapped(object sender, EventArgs args)
        {
            this.CheckedImage.IsVisible = true;
            this.UncheckedImage.IsVisible = false;
            IsChecked = true;
        }
    }
}
