﻿using ProteusMMX.Helpers;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder.Templates.ViewCells
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignatureHistoryViewCell : ViewCell
    {
        public SignatureHistoryViewCell(ref object ParentContext)
        {
            InitializeComponent();
            this.ParentContext = ParentContext;
           
        }
      
        public static readonly BindableProperty ParentContextProperty =
         BindableProperty.Create("ParentContext", typeof(object), typeof(ToolsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }
       
       

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as SignatureHistoryViewCell).ParentContext = newValue;
            }
        }
    }
}