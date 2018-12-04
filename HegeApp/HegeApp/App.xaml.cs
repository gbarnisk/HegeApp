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
        public static IssueManager issueManager { get; set; }

        public App()
        {
            InitializeComponent();


            //System.Console.WriteLine("Hello World");
            issueManager = new IssueManager();

            //issueManager.InitializeTextFile(issueList);
            System.Console.WriteLine("Wow! It worked??");
            MainPage = new MainCarouselPageCS(); //Boots to the C# port of the carousel page.
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
