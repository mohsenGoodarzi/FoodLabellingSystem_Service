using System.ComponentModel.DataAnnotations;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        public string Password { get; set; }

        public ResetPasswordModel() { 
            Email = string.Empty;
            Password = string.Empty;    
        }
    }
}