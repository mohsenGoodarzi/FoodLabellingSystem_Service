﻿using System.ComponentModel.DataAnnotations;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Models
{
    public class User : IUser
    {
        private string _firstName;
        private string _lastName;
        private string _companyName;
        private string _userName;
        private string _password;
        private string _phone;
        private string _email;
        private RoleType _role;
        private StatusType _status;
        private bool _emailConfirmed;
        private bool _phoneConfirmed;
        public User()
        {

            _firstName = string.Empty;
            _lastName = string.Empty;
            _companyName = string.Empty;
            _userName = string.Empty;
            _password = string.Empty;
            _phone = string.Empty;
            _email = string.Empty;
            _role = RoleType.None;
            _emailConfirmed = false;
            _phoneConfirmed = false;
        }

        public User(string firstName, string lastName, string companyName,string userName,
            string password, string email, string phone, StatusType statusType, RoleType role, bool emailConfirmed, bool phoneConfirmed)
        {
            _firstName = firstName;
            _lastName = lastName;
            _companyName = companyName;
            _userName = userName;
            _password = password;
            _phone = phone;
            _email = email;
            _role = role;
            _status = statusType;
            _emailConfirmed = emailConfirmed;
            _phoneConfirmed = phoneConfirmed;

        }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string CompanyName { get => _companyName; set => _companyName = value; }
       
        public string Password { get => _password; set => _password = value; }
        public RoleType Role { get => _role; set => _role = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Email { get => _email; set => _email = value; }
        public StatusType Status { get => _status; set => _status = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public bool IsEmailConfirmed { get => _emailConfirmed; set => _emailConfirmed = value; }
        public bool IsPhoneConfirmed { get => _phoneConfirmed; set => _phoneConfirmed = value; }
    }
}
