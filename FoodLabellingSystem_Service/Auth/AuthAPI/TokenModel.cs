namespace FoodLabellingSystem_Service.Auth.AuthAPI
{
    public class TokenModel
    {
        public TokenModel()
        {
            UserName = "";
            Email = "";
        }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
