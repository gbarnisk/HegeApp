using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HegeApp.Models
{
    class CustomWebView : WebView
    //Shamelessly stolen from https://github.com/xamarin/recipes/tree/master/Recipes/xamarin-forms/Controls/display-pdf
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
            returnType: typeof(string),
            declaringType: typeof(CustomWebView),
            defaultValue: default(string));
        
        public string Uri {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        } //Custom getter and setter for the Uri property
    }
}
