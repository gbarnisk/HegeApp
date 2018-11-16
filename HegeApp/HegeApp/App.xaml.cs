using HegeApp.Models;
using HegeApp.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.DownloadManager;
using HegeApp.Controllers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HegeApp
{
    public partial class App : Application
    {
        public App()
        {
            //The following creates an array of issues for passing into the carousel page. This will be replaced with a startup check and process from google drive.
            

            InitializeComponent();

            //System.Console.WriteLine("Hello World");
            
            IssueManager issueManager = new IssueManager();

            MainPage = new MainCarouselPageCS(issueManager); //Boots to the C# port of the carousel page.
            //MainPage = new TestPage();
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
