namespace FoodLabellingSystem_Service.Models
{
    public class Foods
    {
        public List<Food> AllFoods { get; set; }

        public Foods() {
            AllFoods = new List<Food>();
        }
    }
}
