using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class DishType
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "Dish")]
        public string DishTypeId { get; set; }
        [Required (ErrorMessage ="Member cannot be empty.")]
        [Column("member")]
        [StringLength(50)]
        public string Member { get; set; }

        public ICollection<Food>? Foods { get;   set;}

        public DishType()
        {
            DishTypeId = string.Empty;
            Member = string.Empty;
        }
        public DishType(string dishTypeId, string member)
        {
            DishTypeId = dishTypeId;
            Member = member;

        }
        public override bool Equals(object? obj)
        {
           
            DishType? other = obj as DishType;
                if (other != null) {
                if (other.DishTypeId == DishTypeId && other.Member == Member) { 
                return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return System.HashCode.Combine(DishTypeId, Member);
        }
    }
}

