using NutritionNumerator.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            BindingContext = new AboutViewModel();
        }
    }
}