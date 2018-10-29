using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * A programatic alternative to PDFViewPage.
 */

namespace HegeApp
{
    class PDFViewPageCS : ContentPage
    {
        /*
         * Takes a file name (uri) and constructs a pdf web view page accessing that file.
         */
        public PDFViewPageCS(string uri)
        {
            Padding = new Thickness(0, 0, 0, 0);
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                       Text = uri
                    },
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
}
