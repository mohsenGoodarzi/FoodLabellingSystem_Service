using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodLabellingSystem_Service.Models
{
    public class Unit
    {
        [Key]
        [Display(Name = "unit")]
        [Column("unitId")]
        [StringLength(50)]
        public string UnitId { get; set; }

        [Display(Name = "gram")]
        [Column("toGram")]
        public double ToGram { get; set; }

        public Unit()
        {
            UnitId = string.Empty;
            ToGram = 0.0;
        }

        public Unit(string unitId, double toGram) {

            UnitId = unitId;
            ToGram = toGram;
        }

    }
}

