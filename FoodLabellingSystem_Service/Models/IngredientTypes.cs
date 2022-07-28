namespace FoodLabellingSystem_Service.Models
{
    public class IngredientTypes
    {
        public List<IngredientType> AllIngredientTypes { get; set; }
        public IngredientTypes() {
            AllIngredientTypes = new List<IngredientType>();
        }
    }
}
