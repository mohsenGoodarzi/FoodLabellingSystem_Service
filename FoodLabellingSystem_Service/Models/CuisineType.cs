using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class CuisineType
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "Cuisine")]
        public string CuisineTypeId { get; set; }

        [Required(ErrorMessage ="Member cannot be null")]
        [Column("member")]
        [StringLength(50)]
        public string Member { get; set; }

        public ICollection<Food>? Foods { get; set;}

        public CuisineType(string cuisineTypeId,string member)
        {
            this.CuisineTypeId = cuisineTypeId;
            this.Member = member;
        }

        public CuisineType()
        {
            this.CuisineTypeId = "";
            this.Member = "";
        }
    }
}

