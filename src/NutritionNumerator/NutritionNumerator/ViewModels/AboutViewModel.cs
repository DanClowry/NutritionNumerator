using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionNumerator.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string VersionNumber { get; }
        public ICommand OpenAuthorEmailCommad { get; }

        public AboutViewModel()
        {
            Title = "About";

            VersionNumber = VersionTracking.CurrentVersion;
            OpenAuthorEmailCommad = new Command(() => Launcher.OpenAsync(new Uri("mailto:daniel.clowry2001@gmail.com")));
        }
    }
}