﻿using HegeApp.Models;
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
        public MainCarouselPageCS(List<Issue> issues)
        {
            for (int i = 0; i < issues.Count; i++)
            {
                CustomButton button = new CustomButton
                {
                    pdfURI = issues[i].pdfURI, //The button holds the pdf uri to pass to the pdf view page
                    Text = issues[i].issueName,
                    BackgroundColor = Color.LightGray,
                    BorderWidth = 2,
                    BorderColor = Color.Black,
                    HorizontalOptions = LayoutOptions.Center
                };
                button.Clicked += Button_Clicked;
                
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
                                button
                            }
                        }
                    }
                );
            }
        }

        //When the button is clicked, get the button's uri and load that
        private void Button_Clicked(object sender, EventArgs e)
        {
            CustomButton hackButton = (CustomButton)sender; //Yes there is a better way. I don't feel like learning how to do it.
            Navigation.PushModalAsync(new PDFViewPageCS(hackButton.pdfURI));
        }
    }
}
