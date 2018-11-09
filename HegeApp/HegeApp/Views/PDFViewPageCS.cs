using HegeApp.Models;
using System;
using Xamarin.Forms;

/*
 * A programatic alternative to PDFViewPage.
 */

namespace HegeApp.Views
{
    class PDFViewPageCS : ContentPage
    {
        /*
         * Takes a file name (uri) and constructs a pdf web view page accessing that file.
         */
        public PDFViewPageCS(string uri)
        {
            Button dismissModal = new Button
            {
                Text = "Back",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill,

            };

            dismissModal.Clicked += DismissModal_Clicked;

            Padding = new Thickness(0, 0, 0, 0);
            if (Device.RuntimePlatform == Device.iOS)
            {
                Content = new StackLayout
                {
                    Children =
                {
                    //new Label
                    //{
                    //   Text = uri
                    //},
                    new CustomWebView

                    {
                        Uri = uri,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },

                    dismissModal

                }

                };
            } else if (Device.RuntimePlatform == Device.Android)
            {
                Content = new StackLayout
                {
                    Children =
                {
                    //new Label
                    //{
                    //   Text = uri
                    //},
                    new CustomWebView

                    {
                        Uri = uri,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }

                };
            }
        }

        async void DismissModal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}
