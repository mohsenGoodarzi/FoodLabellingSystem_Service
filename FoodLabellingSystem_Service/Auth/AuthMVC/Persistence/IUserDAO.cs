using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Persistence
{
    public interface IUserDAO
    {
        public List<IUser> GetAll();
        public QueryResult Add(string firstName, string lastName, string companyName, string password, string userName, string email, string phone, string status, string role);
        public QueryResult Remove(string userName);
        public QueryResult Update(string firstName, string lastName, string companyName, string password, string userName, string email, string phone, string status, string role);
        public User GetByEmail(string email);
        public User GetByUserName(string userName);
        public User GetByPhone(string phone);
        public QueryResult ConfirmEmail(string userName);
        public QueryResult ConfirmPhone(string userName);
        public QueryResult UpdatePassword(string password, string userName);

    }
}
