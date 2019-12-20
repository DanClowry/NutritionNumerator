using FoodDataCentral;
using FoodDataCentral.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NutritionNumerator.ViewModels
{
    public class FoodDetailViewModel : BaseViewModel
    {
        private Food food;

        public Food Food
        {
            get { return food; }
            set { SetProperty(ref food, value); }
        }

        private ObservableCollection<FoodNutrient> foodNutrients;

        public ObservableCollection<FoodNutrient> FoodNutrients
        {
            get { return foodNutrients; }
            set { SetProperty(ref foodNutrients, value); }
        }

        FoodDataCentralAPI api = Container.Resolve<FoodDataCentralAPI>();

        public FoodDetailViewModel(int foodId)
        {
            InitPage(foodId);
        }

        async Task InitPage(int foodId)
        {
            await GetFood(foodId);
            Title = Food.Description;

            // Add dictionary elements to ObservableCollection because accessing
            // dictionary values from XAML causes exception
            FoodNutrients = new ObservableCollection<FoodNutrient>
            {
                Food.FoodNutrients[NutrientType.EnergyKcal],
                Food.FoodNutrients[NutrientType.Protein],
                Food.FoodNutrients[NutrientType.Lipid],
                Food.FoodNutrients[NutrientType.CarbohydrateByDifference],
                Food.FoodNutrients[NutrientType.SugarsTotalIncludingNLEA],
                Food.FoodNutrients[NutrientType.FiberTotalDietary],
                Food.FoodNutrients[NutrientType.Sodium]
            };
        }

        async Task GetFood(int fdcId)
        {
            IsBusy = true;

            Food = await api.GetFoodById(fdcId);

            IsBusy = false;
        }
    }
}
