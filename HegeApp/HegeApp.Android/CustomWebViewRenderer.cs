using System.IO;
using System.Net;
using Android.Content;
using HegeApp.Droid;
using HegeApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

/*
 * Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
 * A custom pdf renderer for Android.
 */

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace HegeApp.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        //Included from https://forums.xamarin.com/discussion/106938/context-is-obsolete-as-of-version-2-5 to address a minor error.
        public CustomWebViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                //Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", WebUtility.UrlEncode(Path.Combine(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath, "Issues", customWebView.Uri))));
            }
        }
    }
}