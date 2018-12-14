using HegeApp.Models;
using System;
using Xamarin.Forms;

/*
 * A view page for the issue pdfs. Uses the rendering given by CustomWebViewRenderer depending on platform.
 */

namespace HegeApp.Views
{
    class PDFViewPageCS : ContentPage
    {
        /*
        * Takes an index and constructs a pdf web view page accessing the issueManager's list of that index.
        */
        public PDFViewPageCS(int index)
        {
            //Close
            Button dismissModal = new Button
            {
                Text = "Select Issue",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill,
                FontFamily = "Gill Sans",

            };
            dismissModal.Clicked += DismissModal_Clicked;

            //Determine the correct padding. Stupid notch!
            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                Padding = new Thickness(0, 40, 0, 10);
                dismissModal.TranslationX = 25;
                dismissModal.BackgroundColor = Color.LightGray;
                dismissModal.WidthRequest = 100;

            }
            else if (Device.RuntimePlatform.Equals(Device.Android))
            {
                Padding = new Thickness(0, 0, 0, 0);
            }

            //Construct the page
            Content = new StackLayout
            {
                Children =
                {
                    new CustomWebView
                    {
                        Index = index,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    dismissModal 
                }
            };
        }

        /*
         * Close button clicked method
         */
        async void DismissModal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
