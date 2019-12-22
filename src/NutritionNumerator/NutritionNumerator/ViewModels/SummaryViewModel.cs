using NutritionNumerator.Models.DataStore;
using NutritionNumerator.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutritionNumerator.ViewModels
{
    public class SummaryViewModel : BaseViewModel
    {
        private Day day;

        public Day Day
        {
            get { return day; }
            set { SetProperty(ref day, value); }
        }


        private IDataStore dataStore = Container.Resolve<IDataStore>();

        public SummaryViewModel(DateTime date)
        {
            InitPage(date);
        }

        async Task InitPage(DateTime date)
        {
            Day = await GetDay(date);
            Title = $"Summary {date.ToShortDateString()}";
        }

        public async Task<Day> GetDay(DateTime date)
        {
            IsBusy = true;

            var day = await dataStore.GetDayAsync(date);
            if (day == null)
            {
                await dataStore.SaveDayAsync(new Day() { Date = date, Foods = new List<FoodDS>() });
                day = await dataStore.GetDayAsync(date);
            }
            
            IsBusy = false;

            return day;
        }

        public async Task RefreshDay()
        {
            if (Day == null || Day.Date == null)
            {
                Day = await GetDay(DateTime.Today);
            }
            else
            {
                Day = await GetDay(Day.Date);
            }
        }
    }
}
