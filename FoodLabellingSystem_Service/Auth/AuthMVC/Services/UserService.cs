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

        public Task<List<IUser>> GetAll() {
            return Task.Run(() =>
            {
                return _userADO.GetAll();
            });
        }
        public Task<User> GetByUserName(string userName) {
            return Task.Run(() =>
            {
                return _userADO.GetByUserName(userName);
            });
        }
        public Task<User> GetByPhone(string phone) {
            return Task.Run(() =>
            {
                return _userADO.GetByPhone(phone);
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
                return _userADO.Add(user.FirstName,user.LastName,user.CompanyName, user.UserName,
                    user.Password,user.Email,user.Phone,user.Status.ToString(),user.Role.ToString());
            });
        }

        public Task<QueryResult> Update(User user) {

            return Task.Run(() => {
                return _userADO.Update(user.FirstName, user.LastName, user.CompanyName,user.UserName,
                    user.Password, user.Email, user.Phone, user.Status.ToString(), user.Role.ToString()); 
            });
        }
        public Task<QueryResult> Remove(string userName) {

            return Task.Run(() => { 
               return _userADO.Remove(userName);
            });
        }

        public Task<QueryResult> ConfirmEmail(string userName)
        {
            return Task.Run(() =>
            {
                return _userADO.ConfirmEmail(userName);
            });
        }

        public Task<QueryResult> ConfirmPhone(string userName)
        {
            return Task.Run(() =>
            {
                return _userADO.ConfirmPhone(userName);
            });
        }

        public Task<QueryResult> UpdatePassword(string password, string userName)
        {
            return Task.Run(() =>
            {
            return _userADO.UpdatePassword(password, userName);
            });
        }
    }
}
