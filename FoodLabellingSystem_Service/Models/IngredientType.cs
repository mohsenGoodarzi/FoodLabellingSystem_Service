using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class IngredientType
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "Group")]
        public string? IngredientTypeId { get; set; }

        [Required]
        [Column("member")]
        [StringLength(50)]
        [Display(Name = "Member")]
        public string? Member { get; set; }

        public IngredientType()
        {
            IngredientTypeId = string.Empty;
            Member = string.Empty;
        }
        public IngredientType(string ingredientTypeId, string member)
        {
            IngredientTypeId = ingredientTypeId;
            Member = member; 
        }

    }
}

