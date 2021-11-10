using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DBCores;

namespace PostalCentralSystem
{
    public class DimmensionCategoryType
    {
        [Key]
        public string _id { get; set; }
        public string Code { get; set; }
        public string ParcelTypeCode { get; set; }
        public decimal Price { get; set; }
        public int MinLenght { get; set; }
        public int MaxLenght { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public int MinDepth { get; set; }
        public int MaxDepth { get; set; }
        public int MinWeight { get; set; }
        public int MaxWeight { get; set; }

        /// <summary>
        /// m2
        /// </summary>
        /// <returns></returns>
        public double MinCubature()
        {
            return (double)((double)MinWidth /1000.0) * (double)((double)MinLenght / 1000.0) * (double)((double)MinDepth /1000.0);
        }
        /// <summary>
        /// m2
        /// </summary>
        /// <returns></returns>
        public double MaxCubature()
        {
            return (double)((double)MaxWidth /1000.0) * (double)((double)MaxLenght /1000.0) * (double)((double)MaxDepth /1000.0);
        }
        public int MinSumDimensions()
        {
            return this.MinWidth + this.MinLenght + this.MinDepth;
        }
        public int MaxSumDimensions()
        {
            return this.MaxWidth + this.MaxLenght + this.MaxDepth;
        }

        public bool IsInRange(DimensionsType dimensions)
        {
            bool result = false;


            if (dimensions.Weight  >= this.MinWeight || dimensions.Weight  <= this.MaxWeight)
            {
                if (dimensions.SumDimensions() >= this.MinSumDimensions() || dimensions.SumDimensions() <= this.MaxSumDimensions())
                {
                    result = true;
                }
            }


                //if(weight>= this.MinWeight || weight<= this.MaxWeight)
                //{
                //    if (width >= this.MinWidth  || width <= this.MaxWidth)
                //    {
                //        if (lenght  >= this.MinLenght  || lenght <= this.MaxLenght )
                //        {
                //            if (depth  >= this.MinDepth  || depth <= this.MaxDepth )
                //            {
                //                result = true;
                //            }
                //        }
                //    }
                //}
                return result;
        }


        public DimmensionCategoryType()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<DimmensionCategoryType>("DimensionCategories", this._id, this);
        }

        internal static DimmensionCategoryType GetDimensionCategory(string dimensionCode)
        {
            MongoDbCore MDB = new MongoDbCore();

            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("Code", dimensionCode);

            DimmensionCategoryType o = MDB.GetSingleObject<DimmensionCategoryType>("DimensionCategories", Filter);
            return o;
        }

        internal static List<DimmensionCategoryType> GetDimensionCategory()
        {
            MongoDbCore MDB = new MongoDbCore();


            List<DimmensionCategoryType> o = MDB.GetObjectList<DimmensionCategoryType>("DimensionCategories");
            return o;
        }


    }
}
