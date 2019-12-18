using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionNumerator.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenKeySignupCommand = new Command(() => Launcher.OpenAsync(new Uri("https://api.data.gov/signup")));
        }

        public ICommand OpenKeySignupCommand { get; }

        public async Task<string> GetApiKeyAsync()
        {
            return await SecureStorage.GetAsync("api-key");
        }

        public async Task SetApiKeyAsync(string newKey)
        {
            await SecureStorage.SetAsync("api-key", newKey);
        }
    }
}