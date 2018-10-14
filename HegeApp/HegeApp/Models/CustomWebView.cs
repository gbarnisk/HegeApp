using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
 * A custom WebView with a Uri property for loading files
 */

namespace HegeApp.Models
{
    public class CustomWebView : WebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
                returnType: typeof(string),
                declaringType: typeof(CustomWebView),
                defaultValue: default(string));

        public string Uri {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
    }
}
