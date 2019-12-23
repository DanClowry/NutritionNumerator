using FoodDataCentral;
using NutritionNumerator.Themes;
using NutritionNumerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NutritionNumerator.Services
{
    class SettingsService
    {
        public async Task<string> GetApiKeyAsync()
        {
            return await SecureStorage.GetAsync("api-key");
        }

        public async Task SetApiKeyAsync(string newKey)
        {
            await SecureStorage.SetAsync("api-key", newKey);
            BaseViewModel.Container.Register(new FoodDataCentralAPI(newKey));
        }

        public async Task<bool> GetDarkModeEnabledAsync()
        {
            string value = await SecureStorage.GetAsync("dark-mode-enabled");
            if (value == null)
            {
                await SetDarkModeEnabledAsync(false);
                return false;
            }
            return Boolean.Parse(await SecureStorage.GetAsync("dark-mode-enabled"));
        }

        public async Task SetDarkModeEnabledAsync(bool newValue)
        {
            await SecureStorage.SetAsync("dark-mode-enabled", newValue.ToString());
        }

        public async void ToggleDarkMode()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                if (await GetDarkModeEnabledAsync())
                {
                    Application.Current.Resources = new DarkTheme();
                }
                else
                {
                    Application.Current.Resources = new LightTheme();
                }
            }
        }
    }
}
