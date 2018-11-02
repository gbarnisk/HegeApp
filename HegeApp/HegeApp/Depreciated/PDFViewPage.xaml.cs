using HegeApp.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HegeApp.Depreciated
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PDFViewPage : ContentPage
	{
		public PDFViewPage ()
		{
			InitializeComponent ();
		}

        private void ScreenSwitchPressed(object sender, EventArgs e)
        {
            PDFWebView = new CustomWebView
            {
                Uri = "Hege1.pdf",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand

            };
        }
    }
}