using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Persistence
{
    public interface IUserDAO
    {
        public List<User> GetAll();
        public QueryResult Add(string firstName, string lastName, string companyName, string password, string email, string phone, string status, string role);
        public QueryResult Remove(string firstName, string lastName, string companyName);
        public QueryResult Update(string firstName, string lastName, string companyName, string password, string email, string phone, string status, string role);
        public User GetById(string firstName, string lastName, string companyName);
        public User GetByEmail(string email);


    }
}
