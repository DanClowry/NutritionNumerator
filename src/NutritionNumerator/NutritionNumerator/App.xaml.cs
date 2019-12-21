using System;
using Xamarin.Forms;
using NutritionNumerator.Services;
using NutritionNumerator.Views;
using Xamarin.Essentials;

namespace NutritionNumerator
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected async override void OnStart()
        {
            if (String.IsNullOrWhiteSpace(await SecureStorage.GetAsync("api-key")))
            {
                string key = await MainPage.DisplayPromptAsync("No API Key", "An API key is needed to use the app. Please generate one at https://api.data.gov/signup/");
                try
                {
                    await SecureStorage.SetAsync("api-key", key);
                }
                catch (Exception)
                {
                    await MainPage.DisplayAlert("Unable to save key", "The API key could not be saved", "OK");
                    throw;
                }
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
