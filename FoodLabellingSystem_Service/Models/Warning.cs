using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class Warning
    {
        [Key]
        [Display(Name = "Name")]
        [Column("warningId")]
        [StringLength(50)]
        public string WarningId { get; set; }

        [Column("message", TypeName = "text")]
        public string Message { get; set; }

        [Required(ErrorMessage ="Cannot be Empty")]
        [Display(Name = "Type")]
        [Column("warningType")]
        [StringLength(50)]
        public string WarningType  { get; set; }

        public Warning()
        {
            WarningId = "";
            Message = "";
            WarningType = "";
        }
        public Warning(string warningId, string message, string warningType)
        {

            WarningId = warningId;
            Message = message;
            WarningType = warningType;
        }
    }
}

