using Xamarin.Forms;

/*
 * A custom button object that has a pdfURI property.
 */

namespace HegeApp.Models
{
    class CustomButton : Button
    {
        /*public static readonly BindableProperty pdfURIProperty = BindableProperty.Create<CustomButton, string>(p => p.pdfURI, default(string));
        
        public string pdfURI {
            get { return (string)GetValue(pdfURIProperty); }
            set { SetValue(pdfURIProperty, value); }
        }

        public static readonly BindableProperty IssueProperty = BindableProperty.Create<CustomButton, Issue>(p => p.Issue, default(Issue));
        public Issue Issue
        {
            get { return (Issue)GetValue(IssueProperty); }
            set { SetValue(IssueProperty, value); }
        }*/

        public BindableProperty IndexProperty = BindableProperty.Create<CustomButton, int>(p => p.Index, default(int));
        public int Index 
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
    }
}
