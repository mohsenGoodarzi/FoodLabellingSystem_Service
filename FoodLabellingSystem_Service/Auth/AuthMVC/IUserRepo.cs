using FoodLabellingSystem_Service.Auth.AuthMVC.Models;

namespace FoodLabellingSystem_Service.Auth.AuthMVC
{
    public interface IUserRepo
    {
        public List<IUser> Users { get; }
        public IUser? FindByEmail(string email);
        public IUser? FindByName(string userName);
        public IUser? FindByPhone(string phone);
        public void Add(User user);
        public IUser Update(string userName, User user);
        public void Delete(string userName);
    }
}
