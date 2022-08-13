namespace FoodLabellingSystem_Service.Models
{
    public class UserLogin
    {
        public string IdentefyBy { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public UserLogin() { 
            IdentefyBy =string.Empty;
            Password = string.Empty;
            RememberMe = false;
        }
    }
}
