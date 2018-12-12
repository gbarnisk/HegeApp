using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using ObjCRuntime;
using Plugin.DownloadManager;
using UIKit;
using UserNotifications;
using static System.Console;

namespace HegeApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {


        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            //Asking for push notifications permission
            var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
);
            app.RegisterUserNotificationSettings(notificationSettings);

            //UILocalNotification notification = new UILocalNotification();
            //NSDate.FromTimeIntervalSinceNow(15);
            //notification.AlertAction = "New Hege Issue!";
            //notification.AlertBody = "!!!!!!!!!!!!!!!!!!";
            //notification.ApplicationIconBadgeNumber = 1;
            //notification.SoundName = UILocalNotification.DefaultSoundName;
            //UIApplication.SharedApplication.ScheduleLocalNotification(notification);
           // UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);



            return base.FinishedLaunching(app, options);


            }
 

        /*
         * Based on code from https://github.com/SimonSimCity/Xamarin-CrossDownloadManager 
         */
        public override void HandleEventsForBackgroundUrl(UIApplication application, string sessionIdentifier, Action completionHandler)
        {
            base.HandleEventsForBackgroundUrl(application, sessionIdentifier, completionHandler);
            CrossDownloadManager.BackgroundSessionCompletionHandler = completionHandler;
        }


        //public override void DidEnterBackground(UIApplication app)
        //{
        //    WriteLine("App entering background state.");

        //    nint taskID = 0;
        //    // register a long running task, and then start it on a new thread so that this method can return
        //    taskID = UIApplication.SharedApplication.BeginBackgroundTask(() => {
        //        WriteLine("Running out of time to complete you background task!");
        //        UIApplication.SharedApplication.EndBackgroundTask(taskID);
        //    });
        //    Task.Factory.StartNew(() => FinishLongRunningTask(taskID));
        //}


        //public override void PerformFetch(UIApplication app, Action<UIBackgroundFetchResult> completionHandler)
        //{
        //    WriteLine("GFHFJHGJF");
        //    // Inform system of fetch results
        //    completionHandler(UIBackgroundFetchResult.NewData);
        //}

        //private void FinishLongRunningTask(nint taskID)
        //{
            //WriteLine("Starting task {0}", taskID);
            //WriteLine("Background time remaining: {0}", UIApplication.SharedApplication.BackgroundTimeRemaining);
            //Console.WriteLine("Almost");
            //Thread.Sleep(15000);
            ////UILocalNotification notification = new UILocalNotification();
            ////NSDate.FromTimeIntervalSinceNow(30);
            ////notification.AlertAction = "New Hege Issue!";
            ////notification.AlertBody = "!!!!!!!!!!!!!!!!!!";
            ////UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            //WriteLine("Task {0} finished", taskID);
            //WriteLine("Background time remaining: {0}", UIApplication.SharedApplication.BackgroundTimeRemaining);

           

            // call our end task
            //UIApplication.SharedApplication.EndBackgroundTask(taskID);
          
        }

       
  



    }








   

}

