using Xamarin.Forms;

/*
 * Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
 * A custom WebView with a Uri property for loading files
 */

namespace HegeApp.Models
{
    public class CustomWebView : WebView
    {
        /*public static readonly BindableProperty UriProperty = BindableProperty.Create<CustomWebView, string>(p => p.Uri, default(string));

        public string Uri {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty IssueProperty = BindableProperty.Create<CustomWebView, Issue>(p => p.Issue, default(Issue));
        public Issue Issue
        {
            get { return (Issue)GetValue(IssueProperty); }
            set { SetValue(IssueProperty, value); }
        }*/

        public BindableProperty IndexProperty = BindableProperty.Create<CustomButton, int>(p => p.Index, default(int));
        public int Index {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
    }
}
