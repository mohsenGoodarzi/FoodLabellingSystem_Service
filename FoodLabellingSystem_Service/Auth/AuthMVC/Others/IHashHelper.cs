namespace FoodLabellingSystem_Service.Auth.AuthMVC.Others
{
    public interface IHashHelper
    {
        public string Hash512(string code);
        public string HashRAC(string code);
        public string generateHRPR(string userName);
        public string GenerateRAC();
    }
}