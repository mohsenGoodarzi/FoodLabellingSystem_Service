using FoodLabellingSystem_Service.Auth.AuthMVC.Models;

namespace FoodLabellingSystem_Service.Auth.AuthMVC
{
    public interface IUserRepo
    {
        public List<IUser> Users { get; }
        public IUser FindByEmail(string email);
        public IUser FindByName(string userName);
        public IUser FindByPhone(string phone);
        
    }
}
