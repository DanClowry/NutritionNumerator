﻿using NutritionNumerator.ViewModels;
using System;
using Xamarin.Forms;

namespace NutritionNumerator.Views
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel viewModel;

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SettingsViewModel();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(viewModel.ApiKey))
            {
                await DisplayAlert("Error Saving API Key", "An API must be set to use the app. It cannot be empty.", "OK");
                return;
            }

            try
            {
                await viewModel.SetApiKeyAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Error Saving API Key", "The API key could not be saved.", "OK");
            }
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("Delete All Data?", 
                "Are you sure you want to delete all data? This action cannot be undone.", 
                "Delete", "Cancel");

            if (response)
            {
                await viewModel.DeleteAllDataAsync();
            }
        }

        async void OnToggle(object sender, EventArgs e)
        {
            await viewModel.SetDarkModeEnabledAsync();
        }
    }
}