namespace FoodLabellingSystem_Service.Models
{
    public class DishTypes
    {
        public List<DishType> AllDishTypes { get; set; }

        public DishTypes() {
            AllDishTypes = new List<DishType>();
        }
    }
}
