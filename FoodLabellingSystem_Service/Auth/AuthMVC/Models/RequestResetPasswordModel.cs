namespace FoodLabellingSystem_Service.Auth.AuthMVC.Models
{
    public class RequestResetPasswordModel
    {
        public string ID { get; set; }
        public RequestResetPasswordModel() { 
        ID = string.Empty;
        }
    }
}