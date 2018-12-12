using HegeApp.Models;
using System;
using Xamarin.Forms;

/*
* Takes an index and constructs a pdf web view page accessing the issueManager's list of that index.
*/

namespace HegeApp.Views
{
    class PDFViewPageCS : ContentPage
    {
        public PDFViewPageCS(int index)
        {
            Button dismissModal = new Button
            {
                Text = "Select Issue",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill,

            };

            dismissModal.Clicked += DismissModal_Clicked;

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                Padding = new Thickness(10, 10, 10, 10);
            }
            else if (Device.RuntimePlatform.Equals(Device.Android))
            {
                Padding = new Thickness(0, 0, 0, 0);
            }

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

        async void DismissModal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
