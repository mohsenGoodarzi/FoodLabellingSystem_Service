using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class Ingredient
    {
        [Key]
        [Column("ingredientId")]
        [Display(Name = "ingredient")]
        [StringLength(50)]
        public string IngredientId { get; set; }

        [Required]
        [Column("description")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Ingredient Type")]
        public string IngredientTypeId { get; set; }

        [Required]
        [Column("unitId")]
        [Display(Name = "unit")]
        [StringLength(50)]
        public string UnitId { get; set; }

        [Column("amount")]
        public double Amount { get; set; }

        [Column("fat")]
        public double Fat { get; set; }

        [Column("carbs")]
        public double Carbs { get; set; }

        [Column("protein")]
        public double Protein { get; set; }

        [Column("calory")]
        public double Calory { get; set; }

        [Required]
        [Display(Name = "warning")]
        [Column("warningId")]
        [StringLength(50)]
        public string WarningId { get; set; }


        // public virtual ICollection<FoodIngredient>? FoodIngredients { get; set; }
        public Ingredient(string ingredientId,string description,string ingredientTypeId, string unitId, double amount, double fat, double carbs, double protein, double calory,string warningId)
        {
            IngredientId = ingredientId;
            Description = description;
            IngredientTypeId = ingredientTypeId;
            UnitId = unitId;
            Amount = amount;
            Fat = fat;
            Carbs = carbs;
            Protein = protein;
            Calory = calory;
            WarningId = warningId;
        }
        public Ingredient()
        {
            IngredientId = string.Empty;
            Description = string.Empty;
            IngredientTypeId = string.Empty;
            UnitId = string.Empty;
            Amount = 0.0;
            Fat = 0.0;
            Carbs = 0.0;
            Protein = 0.0;
            Calory = 0.0;
            WarningId = string.Empty;
        }
    }
}

