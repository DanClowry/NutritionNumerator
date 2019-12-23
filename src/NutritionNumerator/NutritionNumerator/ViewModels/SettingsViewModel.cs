using NutritionNumerator.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionNumerator.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public ICommand OpenKeySignupCommand { get; }

        private string apiKey;
        public string ApiKey
        {
            get { return apiKey; }
            private set { apiKey = value; }
        }

        private IDataStore dataStore;
        private SettingsService settingsService;

        public SettingsViewModel()
        {
            Title = "Settings";

            dataStore = Container.Resolve<IDataStore>();
            settingsService = Container.Resolve<SettingsService>();

            OpenKeySignupCommand = new Command(() => Launcher.OpenAsync(new Uri("https://api.data.gov/signup")));
            GetApiKeyAsync();
        }

        public async Task GetApiKeyAsync()
        {
            ApiKey = await settingsService.GetApiKeyAsync();
        }

        public async Task SetApiKeyAsync(string newKey)
        {
            ApiKey = newKey;
            await settingsService.SetApiKeyAsync(ApiKey);
        }

        public async Task DeleteAllDataAsync()
        {
            await dataStore.DeleteAllDataAsync();
        }
    }
}
