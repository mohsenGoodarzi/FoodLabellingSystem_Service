using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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

        public override bool Equals(object? obj)
        {
           
                Unit? unit = obj as Unit;
                if (unit?.UnitId == this.UnitId)
                {
                    return true;
                }
            
            return false;
          
        }
     
        public override int GetHashCode()
        {
            return System.HashCode.Combine(UnitId, ToGram);
        }

        public static bool operator==(Unit a, Unit b)
        {
            
            return (a.UnitId == b.UnitId && a.ToGram == b.ToGram);
        }

        public static bool operator!=(Unit a, Unit b)
        {

            return !(a == b);
        }
    }
}

