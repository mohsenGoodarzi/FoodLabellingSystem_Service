using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class Food
    {
        [Key]
        [Column("foodId")]
        [Display(Name = "Food Name")]
        [StringLength(100)]
        public string FoodId { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Required (ErrorMessage ="Dish Type cannot be empty")]
        [Display(Name = "Dish Type")]
        [Column("dishType")]
        [StringLength(50)]
        public string DishType { get; set; }

        [Required (ErrorMessage = "Cuisine Type cannot be empty")]
        [Display(Name = "Cuisine Type")]
        [Column("cuisineType")]
        [StringLength(50)]
        public string CuisineType { get; set; }

        [Required (ErrorMessage = "Food Type cannot be empty")]
        [Column("foodType")]
        [Display(Name = "Food Type")]
        [StringLength(50)]
        public string FoodType { get; set; }

      //  public ICollection<FoodIngredient>? FoodIngredients { get; set; }

        public Food(string foodId,string description,string dishType, string cuisineType, string foodType)
        {
            FoodId = foodId;
            Description = description;
            DishType = dishType;
            CuisineType = cuisineType;
            FoodType = foodType;
        }

        public Food()
        {
            FoodId = string.Empty;
            Description = string.Empty;
            DishType = string.Empty;
            CuisineType = string.Empty;
            FoodType = string.Empty;
        }
    }
}

