using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Services
{
    public interface IUserService
    {
        public Task<List<IUser>> GetAll();
        public Task<User> GetByEmail(string email);
        public Task<User> GetByUserName(string userName);
        public Task<User> GetByPhone(string phone);
        public Task<QueryResult> Add(User user);
        public Task<QueryResult> Update(User user);
        public Task<QueryResult> Remove(string userName);
        public Task<QueryResult> ConfirmEmail(string userName);
        public Task<QueryResult> ConfirmPhone(string userName);
        public Task<QueryResult> UpdatePassword(string password, string userName);

    }
}
