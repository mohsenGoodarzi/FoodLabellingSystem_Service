using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace FoodLabellingSystem_Service.Auth.AuthMVC.Models
{
    public class EmailModel
    {
        //[Email(ErrorMessage ="Email address cannot be validated")]
        public string Email { get; set; }

        [StringLength(6,ErrorMessage ="The activation code is not valid")]
        public string ActivationCode { get; set; }
        public EmailModel() { 
            Email = string.Empty;
            ActivationCode = string.Empty;
        }
    }
}