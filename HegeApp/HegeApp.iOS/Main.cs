using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using CloudRailSI;

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
            CRCloudRail.AppKey = "5bd9c18b16d0d558c940a685";

            CRGoogleDrive service = new CRGoogleDrive("[1016822824176-6ieibidk36rh7oucvalek1o0gqfv7bbs.apps.googleusercontent.com]", "", "com.companyname.HegeApp:/auth", "SomeState");	                                     
            service.UseAdvancedAuthentication();
			service.Login();
        }
    }
}
