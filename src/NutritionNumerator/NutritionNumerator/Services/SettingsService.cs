using FoodDataCentral;
using NutritionNumerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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
    }
}
