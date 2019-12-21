using NutritionNumerator.Models.DataStore;
using NutritionNumerator.Services;
using System;
using System.Threading.Tasks;

namespace NutritionNumerator.ViewModels
{
    class SummaryViewModel : BaseViewModel
    {
        private Day day;

        public Day Day
        {
            get { return day; }
            set { SetProperty(ref day, value); }
        }


        private IDataStore dataStore;

        public SummaryViewModel()
        {
            Title = "Summary";
            dataStore = Container.Resolve<IDataStore>();
        }

        public async Task GetToday()
        {
            var today = await dataStore.GetDayAsync(DateTime.Today);
            if (today == null)
            {
                await dataStore.SaveDayAsync(new Day() { Date = DateTime.Today });
            }
            Day = await dataStore.GetDayAsync(DateTime.Today);
        }
    }
}
