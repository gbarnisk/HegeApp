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
        private IssueManager IssueManager;

        public MainCarouselPageCS(IssueManager issueManager)
        {
            this.IssueManager = issueManager;

            for (int i = 0; i < issueManager.issueList.Count; i++)
            {
                CustomButton viewButton = new CustomButton
                {
                    pdfURI = issueManager.issueList[i].PdfURI, //The button holds the pdf uri to pass to the pdf view page
                    Text = issueManager.issueList[i].IssueName,
                    BackgroundColor = Color.LightGray,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                viewButton.Clicked += ViewClicked;

                Button downloadButton = new Button
                {
                    Text = "Download",
                    BackgroundColor = Color.LightBlue,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                downloadButton.Clicked += DownloadClicked;

                Label testlabel = new Label
                {
                    Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                };

                Children.Add(
                    new ContentPage
                    {
                        Content = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill,

                            Children =
                            {
                                /*new Image
                                {
                                    Source = issues[i].coverURI,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    VerticalOptions = LayoutOptions.FillAndExpand
                                },*/ //There is an issue with android which is causing crashes when images are loaded.
                                viewButton,
                                downloadButton,
                                //testlabel
                            }
                        }
                    }
                );
            }
        }

        //When the view button is clicked, get the button's uri and load that
        private void ViewClicked(object sender, EventArgs e)
        {
            CustomButton hackButton = (CustomButton)sender; //Yes there is a better way. I don't feel like learning how to do it.
            Navigation.PushModalAsync(new PDFViewPageCS(hackButton.pdfURI));
        }

        /*
         * Downloads the issue into local memory when clicked
         */
        private void DownloadClicked(object sender, EventArgs e)
        {
            IssueManager.DownloadIssueAsync(0);
        }
    }
}
