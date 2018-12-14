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
            
            issueManager = new IssueManager();
            
            MainPage = new MainCarouselPageCS(); //Boots to the issue selection page
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            //When app sleeps and closes normally, mirror all of the issues in the ram into the save state, preserving saved paths.
            issueManager.SaveToLocal(issueManager.issueList, issueManager.filename);
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
