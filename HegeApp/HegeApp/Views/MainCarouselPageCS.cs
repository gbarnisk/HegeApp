using Acr.UserDialogs;
using HegeApp.Controllers;
using HegeApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

/*
 * A carousel of pages with a issue image and view button on them. Pages are swiped left and right to find the correct issue. The view button opens the issue in PDFViewPageCS.
 */

namespace HegeApp.Views
{
    class MainCarouselPageCS : CarouselPage
    {
        /*
         * Constructs a set of contentpages inside a carousel
         */
        public MainCarouselPageCS()
        {
            for (int i = 0; i < App.issueManager.issueList.Count; i++)
            {
                //Open issue button
                CustomButton viewButton = new CustomButton
                {
                    Index = i,
                    Text = App.issueManager.issueList[i].IssueName,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Start,
                    FontFamily = "Gill Sans",


                };
                if (Device.RuntimePlatform.Equals(Device.iOS))
                {

                    viewButton.WidthRequest = 250;
                    viewButton.BackgroundColor = Color.LightGray;
                }
                viewButton.Clicked += ButtonClicked;
                
                //Issue cover image
                Image cover = new Image
                {
                    Source = new Uri(App.issueManager.issueList[i].CoverURL),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                //A single page of the carousel
                ContentPage singleSlide = new ContentPage
                {
                    Content = new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Children =
                        {
                            cover, viewButton
                        }
                    }
                };

                //Correctly pads the page
                if (Device.RuntimePlatform.Equals(Device.iOS))
                {
                    singleSlide.Padding = new Thickness(0, 40, 0, 60);
                }
                else if (Device.RuntimePlatform.Equals(Device.Android))
                {
                    singleSlide.Padding = new Thickness(0, 20, 0, 20);
                }
               

                Children.Add(singleSlide);
            }
        }

        /*
         * Open/download button clicked method. When clicked, it will either download the issue and then open it, if it is not already saved locally, or open it directly, if it is saved.
         */
        private async void ButtonClicked(object sender, EventArgs e)
        {
            CustomButton button = (CustomButton)sender;
            int index = button.Index;
            if (!App.issueManager.issueList[index].PdfLocal) //It's not saved locally
            {
                using (UserDialogs.Instance.Loading("Downloading issue..."))
                {
                    await App.issueManager.DownloadIssueAsync(index);
                }
            }

            Navigation.PushModalAsync(new PDFViewPageCS(index));
        }
    }
}
