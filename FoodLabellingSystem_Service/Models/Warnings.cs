namespace FoodLabellingSystem_Service.Models
{
    public class Warnings
    {
        public List<Warning> AllWarnings { get; set; }
        public Warnings() {

            AllWarnings = new List<Warning>();
           
        }
    }
}
