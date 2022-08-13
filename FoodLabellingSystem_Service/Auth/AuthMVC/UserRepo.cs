using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Auth.AuthMVC.Persistence;
using FoodLabellingSystem_Service.Auth.AuthMVC.Services;
using FoodLabellingSystem_Service.Other;

namespace FoodLabellingSystem_Service.Auth.AuthMVC
{
    public class UserRepo : IUserRepo
    {
      
        private readonly IUserService _userService;
        public List<IUser> Users
        {
            get
            {
                return _userService.GetAll().Result;
            }
        }
        public  UserRepo( IUserService userService)
        {
            _userService = userService;
           
        }
       
        public IUser FindByEmail(string email)
        {
             var user = (from myUser in Users
                    where myUser.Email == email
                    select myUser).FirstOrDefault();

            return user != null ? user : new User();
        }

         public IUser FindByName(string userName)
        {
            var user = (from myUser in Users
                    where myUser.UserName == userName
                    select myUser).FirstOrDefault();

            return user != null ? user : new User();
        }

        public IUser FindByPhone(string phone)
        {
            var user = (from myUser in Users
                    where myUser.Phone == phone
                    select myUser).FirstOrDefault();

            return user != null ? user : new User();
        }

      
    }
}
