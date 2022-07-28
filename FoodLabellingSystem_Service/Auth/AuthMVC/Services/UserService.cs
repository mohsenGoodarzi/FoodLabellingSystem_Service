using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Auth.AuthMVC.Persistence;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Services
{
    public class UserService:IUserService
    {
        private readonly IUserDAO _userADO;
        public UserService(IUserDAO userADO)
        {
            _userADO = userADO;
        }

        public Task<User> GetById(string firstName, string lastName, string companyName) {

            return Task.Run(() => {
                return _userADO.GetById(firstName, lastName, companyName);
            });
        }

        public Task<User> GetByEmail(string email)
        {
            return Task.Run(() =>
            {

                return _userADO.GetByEmail(email);
            });
        }

        public Task<QueryResult> Add(User user) {
            return Task.Run(() => {
                return _userADO.Add(user.FirstName,user.LastName,user.CompanyName,
                    user.Password,user.Email,user.Phone,user.Status.ToString(),user.Role.ToString());
            });
        }

        public Task<QueryResult> Update(User user) {

            return Task.Run(() => {
                return _userADO.Update(user.FirstName, user.LastName, user.CompanyName,
                    user.Password, user.Email, user.Phone, user.Status.ToString(), user.Role.ToString()); 
            });
        }
        public Task<QueryResult> Remove(string firstName, string lastName, string companyName) {

            return Task.Run(() => { 
               return _userADO.Remove(firstName, lastName, companyName);
            });
        }

    }
}
