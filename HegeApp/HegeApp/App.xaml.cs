using HegeApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HegeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PDFView(); //Boots to PDF View page
			//MainPage = new MainCarouselPage();
		}
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
