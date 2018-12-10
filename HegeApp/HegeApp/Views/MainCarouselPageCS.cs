﻿using HegeApp.Controllers;
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
                viewButton.Clicked += ViewClicked;

                Button downloadButton = new CustomButton
                {
                    Text = "Download",
                    Index = i,
                    BackgroundColor = Color.LightBlue,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                downloadButton.Clicked += DownloadClicked;

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
                                viewButton,
                                downloadButton,
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
            Navigation.PushModalAsync(new PDFViewPageCS(/*hackButton.pdfURI, hackButton.Issue, */hackButton.Index));
        }

        /*
         * Downloads the issue into local memory when clicked
         */
        private void DownloadClicked(object sender, EventArgs e)
        {
            System.Console.WriteLine("GRIFFIN'S DEBUG Download button clicked");
            CustomButton hackButton = (CustomButton)sender;
            App.issueManager.DownloadIssueAsync(hackButton.Index);
        }
    }
}
