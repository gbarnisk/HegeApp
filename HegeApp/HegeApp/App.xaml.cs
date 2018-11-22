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
            List<Issue> issueList= new List<Issue>();
            issueList.Add(new Issue("Life on the Hege", "https://macalesterhegemonocle.files.wordpress.com/2018/11/v2_i1.pdf", "Life_on_the_hege.png", true, "https://macalesterhegemonocle.files.wordpress.com/2018/11/v9_i2.pdf", "Hege1.pdf", true));
            issueList.Add(new Issue("The Hege Gets a Job", "", "The_Hege_gets_a_job.png", true, "", "Hege2.pdf", true));
            issueList.Add(new Issue("The Last Minute Issue", "", "The_Last_Minute_Issue.png", true, "", "Hege3.pdf", true));
            System.Console.WriteLine("Oh La La");
            System.Console.WriteLine(issueList[0]);
            //The following creates an array of issues for passing into the carousel page. This will be replaced with a startup check and process from google drive.


            InitializeComponent();


            //System.Console.WriteLine("Hello World");
            IssueManager issueManager = new IssueManager();

            issueManager.InitializeTextFile(issueList);
            System.Console.WriteLine("Wow! It worked??");
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
