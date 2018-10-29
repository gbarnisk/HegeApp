using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace HegeApp
{
    public partial class MainCarouselPage : CarouselPage
    {
        public MainCarouselPage()
        {
            InitializeComponent();
            MyImage.Source = ImageSource.FromFile("Life_on_the_hege.png");
            MyImage2.Source = ImageSource.FromFile("The_Hege_gets_a_job.png");
            MyImage3.Source = ImageSource.FromFile("The_Last_Minute_Issue.png");

        }

        void Handle_Clicked_0(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            Navigation.PushModalAsync(new PDFViewPageCS("Hege1.pdf"));
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            Navigation.PushModalAsync(new PDFViewPageCS("Hege2.pdf"));
        }

        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            //throw new NotImplementedException();
            Navigation.PushModalAsync(new PDFViewPageCS("Hege3.pdf"));
        }
    }
}
