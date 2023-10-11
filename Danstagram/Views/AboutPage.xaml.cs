using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Danstagram.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            Console.WriteLine("Started Loading AboutPage");
            InitializeComponent();
            Console.WriteLine("Finished Loading AboutPage");
        }
    }
}