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
using System.IO;

namespace NutritionNumerator.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static TinyIoCContainer Container { get; set; } = new TinyIoCContainer();

        static BaseViewModel()
        {
            var localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Combine(localAppPath, "Food.db3");

            Container.Register(new SettingsService());
            var settings = Container.Resolve<SettingsService>();
            if (!String.IsNullOrWhiteSpace(settings.GetApiKeyAsync().Result))
            {
                string key = settings.GetApiKeyAsync().Result;
                Container.Register(new FoodDataCentralAPI(key));
            }

            Container.Register<IDataStore>(new SqliteDataStore(dbPath));
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
