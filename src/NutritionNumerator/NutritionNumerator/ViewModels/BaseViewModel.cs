using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using NutritionNumerator.Models;
using NutritionNumerator.Services;
using TinyIoC;
using FoodDataCentral;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NutritionNumerator.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static TinyIoCContainer Container { get; set; } = new TinyIoCContainer();

        static BaseViewModel()
        {
            string key = Task.Run(async () => await SecureStorage.GetAsync("api-key")).Result;

            Container.Register(new FoodDataCentralAPI(key));
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
