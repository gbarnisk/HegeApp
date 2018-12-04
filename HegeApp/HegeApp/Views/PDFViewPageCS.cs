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
        public PDFViewPageCS(/*string uri, Issue issue, */int index)
        {
            Button dismissModal = new Button
            {
                Text = "Select Issue",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill,

            };

            dismissModal.Clicked += DismissModal_Clicked;

            Padding = new Thickness(0, 20, 0, 10);
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
                        //Uri = uri,
                        //Issue = issue,
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
