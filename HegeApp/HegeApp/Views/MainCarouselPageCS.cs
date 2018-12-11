using Acr.UserDialogs;
using HegeApp.Controllers;
using HegeApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

/*
 * A C# port of MainCarouselPage.xaml. Made to test the functionality of Issue objects.
 */

namespace HegeApp.Views
{
    class MainCarouselPageCS : CarouselPage
    {

        public MainCarouselPageCS()
        {
            for (int i = 0; i < App.issueManager.issueList.Count; i++)
            {
                CustomButton viewButton = new CustomButton
                {
                    Index = i,
                    Text = App.issueManager.issueList[i].IssueName,
                    BackgroundColor = Color.LightGray,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                viewButton.Clicked += ButtonClicked;

                Image cover = new Image
                {
                    Source = new Uri(App.issueManager.issueList[i].CoverURL),
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };

                Children.Add(
                    new ContentPage
                    {
                        Padding = new Thickness(0, 20, 0, 0),
                        Content = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill,
                            Children =
                            {
                                cover,
                                viewButton
                            }
                        }
                    }
                );
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
