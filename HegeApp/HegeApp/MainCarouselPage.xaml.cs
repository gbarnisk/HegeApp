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
