using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    [Table("Food_Ingredient")]
    public class FoodIngredient
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Food Name")]
        [Column("foodId")]
        [StringLength(100)]
        public string FoodId { get; set; }

        [Required]
        [Display(Name = "Ingredient Name")]
        [Column("ingredientId")]
        [StringLength(50)]
        public string IngredientId { get; set; }

        [Required]
        [Display(Name = "Unit")]
        [Column("unitId")]
        [StringLength(50)]
        public string UnitId { get; set; }

        [Column("amount")]
        public double Amount { get; set; }

        [Column("fat")]
        public double? Fat { get; set; }

        [Column("carbs")]
        public double? Carbs { get; set; }

        [Column("protein")]
        public double? Protein { get; set; }

        [Column("calory")]
        public double? Calory { get; set; }

        public FoodIngredient()
        {
            Id = 0;
            FoodId = string.Empty;
            IngredientId = string.Empty;
            UnitId = string.Empty;
            Amount = 0.0;
        }
        public FoodIngredient(int id, string foodId, string ingredientId, string unitId, double amount)
        {
            Id = id;
            FoodId = foodId;
            IngredientId = ingredientId;
            UnitId = unitId;
            Amount = amount;
        }
    }
}

