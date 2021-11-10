using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    public class DimensionsType
    {
        [Key]
        public string _id { get; set; }
        public int Weight { get; set; }
        public int Width { get; set; }
        public int Lenght { get; set; }
        public int Depth { get; set; }
        public DimmensionCategoryType DimensionCategory { get; set; }

        /// <summary>
        /// m2
        /// </summary>
        /// <returns></returns>
        public double Cubature()
        { 
            return (double)((double)Width/1000.0) * (double)((double)Lenght / 1000.0) * (double)((double)Depth / 1000.0);
        }
        public int SumDimensions()
        {
            return this.Width + this.Lenght + this.Depth;
        }

        public bool SetDimensionCategory(List<DimmensionCategoryType>DimCategories = null)
        {
            bool result = false;

            if (DimCategories == null)
                DimCategories = DimmensionCategoryType.GetDimensionCategory();

            foreach(var dc in DimCategories)
            {
                if (dc.IsInRange(this) == true)
                {
                    this.DimensionCategory = dc;
                    return true;
                }
            }



            return result;
        }


    }
}
