using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HegeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PDFView : ContentPage
    {
        public PDFView()
        {
            InitializeComponent();
        }

        //This method is apparently not being found properly. It appears to not do anything, so I have created this empty method to remove the error.
        //TODO: figure out what the heck InitializeComponent is and fix this properly
        private void InitializeComponent()
        {
        }
    }
}