using Xamarin.Forms;

/*
 * A custom button object that has an index property for use by xml.
 */

namespace HegeApp.Models
{
    class CustomButton : Button
    {
        public BindableProperty IndexProperty = BindableProperty.Create<CustomButton, int>(p => p.Index, default(int));
        public int Index 
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
    }
}
