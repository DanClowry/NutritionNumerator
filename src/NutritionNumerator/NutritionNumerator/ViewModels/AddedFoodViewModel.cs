using NutritionNumerator.Models.DataStore;
using NutritionNumerator.Services;
using System.Threading.Tasks;

namespace NutritionNumerator.ViewModels
{
    public class AddedFoodViewModel : BaseViewModel
    {
        private Day day;
        public Day Day
        {
            get { return day; }
            set { SetProperty(ref day, value); }
        }

        public AddedFoodViewModel(Day day)
        {
            Day = day;
        }

        public async Task DeleteFood(FoodDS food)
        {
            Day.Foods.Remove(food);
            var dataStore = Container.Resolve<IDataStore>();
            await dataStore.SaveDayAsync(Day);
            Day = await dataStore.GetDayAsync(Day.Id);
        }
    }
}
