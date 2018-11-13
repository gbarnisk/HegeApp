using HegeApp.Models;
using HegeApp.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HegeApp
{
    public partial class App : Application
    {
        public App()
        {
            //The following creates an array of issues for passing into the carousel page. This will be replaced with a startup check and process from google drive.
            var issueList = new List<Issue>();
            issueList.Add(new Issue("Life_on_the_hege", "Life_on_the_hege.png", "Hege1.pdf"));
            issueList.Add(new Issue("The_Hege_gets_a_job", "The_Hege_gets_a_job.png", "Hege2.pdf"));
            issueList.Add(new Issue("The_Last_Minute_Issue", "The_Last_Minute_Issue.png", "Hege3.pdf"));

            InitializeComponent();
            System.Console.WriteLine("Hello World");
            MainPage = new MainCarouselPageCS(issueList); //Boots to the C# port of the carousel page.
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
