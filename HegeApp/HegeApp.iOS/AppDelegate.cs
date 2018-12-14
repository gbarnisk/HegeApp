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
    }
}