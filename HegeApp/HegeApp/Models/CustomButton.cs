using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

/*
 * A custom button object that has a pdfURI property.
 */

namespace HegeApp.Models
{
    class CustomButton : Button
    {
        public static readonly BindableProperty pdfURIProperty = BindableProperty.Create<CustomButton, string>(p => p.pdfURI, default(string));

        public string pdfURI {
            get { return (string)GetValue(pdfURIProperty); }
            set { SetValue(pdfURIProperty, value); }
        }
    }
}
