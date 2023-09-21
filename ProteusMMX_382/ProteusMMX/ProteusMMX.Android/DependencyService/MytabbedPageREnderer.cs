using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Tabs;
using ProteusMMX.Droid.DependencyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(MytabbedPageREnderer))]
namespace ProteusMMX.Droid.DependencyService
{
    public class MytabbedPageREnderer : TabbedPageRenderer
    {
        public MytabbedPageREnderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null || e.OldElement != null)
                return;
            TabLayout tablayout = (TabLayout)ViewGroup.GetChildAt(1);
            Android.Views.ViewGroup vgroup = (Android.Views.ViewGroup)tablayout.GetChildAt(0);
            for (int i = 0; i < vgroup.ChildCount; i++)
            {
                Android.Views.ViewGroup vvgroup = (Android.Views.ViewGroup)vgroup.GetChildAt(i);
                //Typeface fontFace = Typeface.CreateFromAsset(this.Context.Assets, "b.ttf");
                for (int j = 0; j < vvgroup.ChildCount; j++)
                {
                    Android.Views.View vView = (Android.Views.View)vvgroup.GetChildAt(j);
                    if (vView.GetType() == typeof(AppCompatTextView) || vView.GetType() == typeof(TextView))
                    {

                        TextView txtView = (TextView)vView;
                        //disable the upper case
                        txtView.SetAllCaps(false);
                        //set font for textview
                        //txtView.SetTypeface(fontFace, TypefaceStyle.Normal);
                    }
                }
            }
        }
    }
}