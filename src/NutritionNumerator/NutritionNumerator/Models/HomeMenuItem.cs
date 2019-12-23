namespace NutritionNumerator.Models
{
    public enum MenuItemType
    {
        Summary,
        Browse,
        History,
        About,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
