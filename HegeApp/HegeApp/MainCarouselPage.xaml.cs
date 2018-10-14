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
        }
    }
}
