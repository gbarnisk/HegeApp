﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using HegeApp.Droid;
using System.Net;
using HegeApp.Models;

/*
 * Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
 * A custom pdf renderer for Android.
 */

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace HegeApp.Droid
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        //Included from https://forums.xamarin.com/discussion/106938/context-is-obsolete-as-of-version-2-5 to address an error
        public CustomWebViewRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}",
                    string.Format("file:///android_asset/Content/{0}",
                    WebUtility.UrlEncode(customWebView.Uri))));
            }
        }
    }
}