using Xamarin.Forms;

namespace ProteusMMX.DependencyInterface
{
    public class MyPicker : Picker
    {
        public static readonly BindableProperty BottomLeftProperty = BindableProperty.Create(
      propertyName: "",
      returnType: typeof(int),
      declaringType: typeof(MyPicker),
      defaultValue: default(int));

        public int BottomLeft
        {
            get { return (int)GetValue(BottomLeftProperty); }
            set { SetValue(BottomLeftProperty, value); }
        }
    }
}