using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.IO;
using System.Linq;

namespace HegeApp.Droid
{
    [Activity(Label = "HegeApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            UserDialogs.Init(this);

            //Following from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager
            CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file =>
            {
                string fileName = Android.Net.Uri.Parse(file.Url).Path.Split('/').Last();
                //string newPath = Path.Combine(ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);
                string newPath = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath, "Issues", fileName); //From https://kimsereyblog.blogspot.com/2016/11/differences-between-internal-and.html
                System.Console.WriteLine("GRIFFIN'S DEBUG File path: " + newPath);
                return newPath;
            });
        }
    }
}

//Will's test comment
//Trever's test comment
//Griffin's comment
//Nathan's comment