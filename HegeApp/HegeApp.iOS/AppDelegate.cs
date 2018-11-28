﻿using System;
using System.Collections.Generic;
using System.Linq;

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


                //if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                //{
                //    var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                //        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                //    );

                //    app.RegisterUserNotificationSettings(notificationSettings);
                //}

                // check for a notification

                //if (options != null)
                //{
                //    // check for a local notification
                //    if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
                //    {
                //        var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                //        if (localNotification != null)
                //        {
                //            UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
                //            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                //            Window.RootViewController.PresentViewController(okayAlertController, true, null);

                //            // reset our badge
                //            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                //        }
                //    }
                //}

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


        //public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        //{
        //    // show an alert
        //    UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
        //    okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

        //    UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(okayAlertController, true, null);

        //    // reset our badge
        //    UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        //}
    }
}
