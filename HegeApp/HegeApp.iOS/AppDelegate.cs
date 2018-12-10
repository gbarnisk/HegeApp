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
            // Request notification permissions from the user
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) => {
                // Handle approval

                //                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                //                UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                //);
                //app.RegisterUserNotificationSettings(notificationSettings);

                //UILocalNotification notification = new UILocalNotification();
                //NSDate.FromTimeIntervalSinceNow(5);
                //notification.AlertAction = "Hege Time!!";
                //notification.AlertBody = "You have a new isuue!";
                //UIApplication.SharedApplication.ScheduleLocalNotification(notification);
                //notification.ApplicationIconBadgeNumber = 1;
                //notification.SoundName = UILocalNotification.DefaultSoundName;
                //UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);

                //check for a notification

                //if (options != null)
                //{
                //// check for a local notification
                //if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
                //{
                //    var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                //    if (localNotification != null)
                //    {
                //        UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
                //        okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                //        Window.RootViewController.PresentViewController(okayAlertController, true, null);

                //        // reset our badge
                //        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                //    }
                //}
                //    }

                });



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


        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            // show an alert
            UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(okayAlertController, true, null);

            // reset our badge
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }
        public override void DidEnterBackground(UIApplication app)
        {
            Console.WriteLine("App entering background state.");

            nint taskID = 0;
            // if you're creating a VOIP application, this is how you set the keep alive
            //UIApplication.SharedApplication.SetKeepAliveTimout(600, () => { /* keep alive handler code*/ });

            // register a long running task, and then start it on a new thread so that this method can return
            taskID = UIApplication.SharedApplication.BeginBackgroundTask(() => {
                Console.WriteLine("Running out of time to complete you background task!");
                UIApplication.SharedApplication.EndBackgroundTask(taskID);
            });
            Task.Factory.StartNew(() => FinishLongRunningTask(taskID));
        }

        private void FinishLongRunningTask(nint taskID)
        {
            Console.WriteLine("Starting task {0}", taskID);
            Console.WriteLine("Background time remaining: {0}", UIApplication.SharedApplication.BackgroundTimeRemaining);

          

            Console.WriteLine("Task {0} finished", taskID);
            Console.WriteLine("Background time remaining: {0}", UIApplication.SharedApplication.BackgroundTimeRemaining);

           

            // call our end task
            UIApplication.SharedApplication.EndBackgroundTask(taskID);
          
        }

        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
  // Check for new data, and display it

  
  // Inform system of fetch results
  completionHandler(UIBackgroundFetchResult.NewData);
        }
    }
}

