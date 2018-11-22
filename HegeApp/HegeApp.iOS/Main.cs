using Foundation;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System;
using System.IO;
using UIKit;

namespace HegeApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");

            //Following from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager
            CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file =>
            {
                string fileName = (new NSUrl(file.Url, false)).LastPathComponent;
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
            });
        }
    }
}
