using NutritionNumerator.Services;
using NutritionNumerator.Themes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionNumerator.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public ICommand OpenKeySignupCommand { get; }
        
        private bool isDarkMode;
        public bool IsDarkMode
        {
            get { return isDarkMode; }
            set { SetProperty(ref isDarkMode, value); }
        }


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
            GetDarkModeEnabledAsync();
        }

        public async Task GetDarkModeEnabledAsync()
        {
            IsDarkMode = await settingsService.GetDarkModeEnabledAsync();
            settingsService.ToggleDarkMode();
        }

        public async Task SetDarkModeEnabledAsync()
        {
            await settingsService.SetDarkModeEnabledAsync(IsDarkMode);
            settingsService.ToggleDarkMode();
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
