//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("ProteusMMX.Views.Common.CreateWorkorderFromInspectionPage.xaml", "Views/Common/CreateWorkorderFromInspectionPage.xaml", typeof(global::ProteusMMX.Views.Common.CreateWorkorderFromInspectionPage))]

namespace ProteusMMX.Views.Common {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\Common\\CreateWorkorderFromInspectionPage.xaml")]
    public partial class CreateWorkorderFromInspectionPage : global::Rg.Plugins.Popup.Pages.PopupPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label DescriptionLabel;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::ProteusMMX.DependencyInterface.CustomEditor Description;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label AdditionalDetailsLabel;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::ProteusMMX.DependencyInterface.CustomEditor AdditionalDetails;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button CreateWo;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(CreateWorkorderFromInspectionPage));
            DescriptionLabel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "DescriptionLabel");
            Description = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::ProteusMMX.DependencyInterface.CustomEditor>(this, "Description");
            AdditionalDetailsLabel = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "AdditionalDetailsLabel");
            AdditionalDetails = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::ProteusMMX.DependencyInterface.CustomEditor>(this, "AdditionalDetails");
            CreateWo = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "CreateWo");
        }
    }
}
