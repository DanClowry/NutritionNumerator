using NutritionNumerator.Models.DataStore;
using NutritionNumerator.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NutritionNumerator.ViewModels
{
    class HistoryViewModel : BaseViewModel
    {
        public ObservableCollection<Day> Days { get; private set; }
        public ICommand GetDaysCommand { get; }

        private IDataStore dataStore = Container.Resolve<IDataStore>();

        public HistoryViewModel()
        {
            Title = "History";

            Days = new ObservableCollection<Day>();
            GetDaysCommand = new Command(async () => { await GetDaysAsync(); });
            GetDaysAsync();
        }

        async Task GetDaysAsync()
        {
            IsBusy = true;

            var days = await dataStore.GetDaysAsync();
            days = days.OrderBy(d => d.Date).ThenBy(d => d.Date).Reverse().ToList();

            Days.Clear();
            foreach (Day day in days)
            {
                Days.Add(day);
            }

            IsBusy = false;
        }
    }
}
