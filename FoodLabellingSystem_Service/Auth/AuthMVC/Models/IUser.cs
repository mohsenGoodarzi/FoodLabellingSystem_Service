namespace FoodLabellingSystem_Service.Auth.AuthMVC.Models
{
    public interface IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public StatusType Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }  

    }
}
