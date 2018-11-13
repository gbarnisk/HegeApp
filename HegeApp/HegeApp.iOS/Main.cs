using UIKit;
using CloudRailSI;

namespace HegeApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            CRCloudRail.AppKey = "5bd9c18b16d0d558c940a685";
            System.Console.WriteLine("app key");
            CRDropbox service = new CRDropbox("EKFhBHnV6xAAAAAAAAAAHtnOPL3NwBpuCbOhLyWkcLejzM-A-B3ZHtWusqoKKp9X", "o7iki0jg52f4pnn");
            System.Console.WriteLine("db key 1");
            service.UseAdvancedAuthentication();
            System.Console.WriteLine("advanced authentication");
            service.Login();
            System.Console.WriteLine("login");
             //if you want to use a different Application Delegate class from "AppDelegate"
             //you can specify it here.

            //CRGoogleDrive shilad = new CRGoogleDrive("1016822824176-6ieibidk36rh7oucvalek1o0gqfv7bbs.apps.googleusercontent.com", "", "", "state");
            //System.Console.WriteLine("db key");
            //shilad.UseAdvancedAuthentication();
            //System.Console.WriteLine("advanced authentication");
            //shilad.Login();
            //System.Console.WriteLine("login");
            //UIApplication.Main(args, null, "AppDelegate");


            CRCloudMetaData[] result = Foundation.NSArray.FromArray<CRCloudMetaData>(service.ChildrenOfFolderWithPath(
                "/Apps/HegeApp"));

        }
    }
}
