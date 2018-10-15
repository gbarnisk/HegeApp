using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Foundation;
using HegeApp.iOS;
using HegeApp;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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
            }
            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}