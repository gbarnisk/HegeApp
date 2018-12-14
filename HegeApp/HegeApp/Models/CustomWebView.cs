using Xamarin.Forms;

/*
 * Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
 * A custom WebView with an index property, for use in xml
 */

namespace HegeApp.Models
{
    public class CustomWebView : WebView
    {
        public BindableProperty IndexProperty = BindableProperty.Create<CustomButton, int>(p => p.Index, default(int));
        public int Index {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
    }
}
