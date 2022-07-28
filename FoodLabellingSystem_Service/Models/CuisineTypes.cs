namespace FoodLabellingSystem_Service.Models
{
    public class CuisineTypes
    {
        public List<CuisineType> AllCuisineTypes { get; set; }
        public CuisineTypes() {
            AllCuisineTypes = new List<CuisineType>();
            }
    }
}
