namespace FoodLabellingSystem_Service.Models
{
    public class FoodIngredients
    {
        public List<FoodIngredient> AllFoodIngredients { get; set; }
        public FoodIngredients() {
            AllFoodIngredients = new List<FoodIngredient>();
        }
    }
}
