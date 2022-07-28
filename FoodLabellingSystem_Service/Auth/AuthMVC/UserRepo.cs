using FoodLabellingSystem_Service.Auth.AuthMVC.Models;
using FoodLabellingSystem_Service.Auth.AuthMVC.Persistence;

namespace FoodLabellingSystem_Service.Auth.AuthMVC
{
    public class UserRepo : IUserRepo
    {
        private List<IUser> _users;

        public List<IUser> Users { get => _users; }
        public UserRepo()
        {
            _users = new List<IUser>() {
            new User("AF","AL","AA","AA@gmail.com","HashedPassword","10000000001","AA@gmail.com",RoleType.Administrator,StatusType.Registered),
            new User("BF","BL","BB","BB@gmail.com","HashedPassword","10000000002","BB@gmail.com",RoleType.Administrator,StatusType.Suspended),
            new User("CF","CL","CC","CC@gmail.com","HashedPassword","10000000003","CC@gmail.com",RoleType.Administrator,StatusType.Activated),
            new User("DF","DL","DD","DD@gmail.com","HashedPassword","10000000004","DD@gmail.com",RoleType.Administrator,StatusType.Closed)
            };
        }
        public UserRepo( IUserDAO userDAO)
        {
            _users = new List<IUser>() {
            new User("AF","AL","AA","AA@gmail.com","HashedPassword","10000000001","AA@gmail.com",RoleType.Administrator,StatusType.Registered),
            new User("BF","BL","BB","BB@gmail.com","HashedPassword","10000000002","BB@gmail.com",RoleType.Administrator,StatusType.Suspended),
            new User("CF","CL","CC","CC@gmail.com","HashedPassword","10000000003","CC@gmail.com",RoleType.Administrator,StatusType.Activated),
            new User("DF","DL","DD","DD@gmail.com","HashedPassword","10000000004","DD@gmail.com",RoleType.Administrator,StatusType.Closed)
            };
        }
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {

                var user = FindByEmail(userName);
                if (user != null)
                {
                    _users.Remove(user);
                }
            }

        }

        public IUser? FindByEmail(string email)
        {
            return (from myUser in _users
                    where myUser.Email == email
                    select myUser).FirstOrDefault();
        }

         public IUser? FindByName(string userName)
        {
            return (from myUser in _users
                    where myUser.UserName == userName
                    select myUser).FirstOrDefault();
        }

        public IUser? FindByPhone(string phone)
        {
            return (from myUser in _users
                    where myUser.Phone == phone
                    select myUser).FirstOrDefault();
        }

        public IUser Update(string userName, User user)
        {
            var foundUser = (from myUser in _users
                             where myUser.UserName == userName
                             select myUser).FirstOrDefault();

            if (foundUser != null)
            {
                foundUser.FirstName = user.FirstName;
                foundUser.LastName = user.LastName;
                foundUser.UserName = user.UserName;
                foundUser.Password = user.Password;
                foundUser.Email = user.Email;
                foundUser.Phone = user.Phone;
                foundUser.Role = user.Role;
                foundUser.Status = user.Status;
                return foundUser;
            }

            return new User();
        }

      
    }
}
