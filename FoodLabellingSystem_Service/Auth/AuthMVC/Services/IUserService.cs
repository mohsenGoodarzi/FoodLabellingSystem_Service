using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Services
{
    public interface IUserService
    {
        public Task<User> GetById(string firstName, string lastName, string companyName);
        public Task<User> GetByEmail(string email);
        public Task<QueryResult> Add(User user);
        public Task<QueryResult> Update(User user);
        public Task<QueryResult> Remove(string firstName, string lastName, string companyName);

    }
}
