using System.IO;
using System.Net;
using HegeApp.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using HegeApp.Models;
using System;

/*
 * Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
 * Custom renderer for PDFs in webview for iOS.
 */

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace HegeApp.iOS
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                string filePath = App.issueManager.issueList[customWebView.Index].PdfPath;
                System.Console.WriteLine("GRIFFIN'S DEBUG This is the fileName attempted to be opened: " + filePath);
                System.Console.WriteLine("GRIFFIN'S DEBUG And this is the NSUrl attempted to be opened: " + new NSUrl(filePath, false));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(filePath, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}



