using DBCores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalCentralSystem
{
    public class ParcelKindType
    {
        public string _id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool isDefault { get; set; }
        public PunctualityParametersType Punctuality { get; set; }

        public ParcelKindType()
        {
            this._id = Guid.NewGuid().ToString();
        }


        public static ParcelKindType GetKind(string StatusCode)
        {
            ParcelKindType status = new ParcelKindType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("Code", StatusCode);
            status = MDB.GetSingleObject<ParcelKindType>("ParcelKinds", Filter);

            return status;
        }

        public static ParcelKindType GetDefault()
        {
            ParcelKindType status = new ParcelKindType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("isDefault", true);
            status = MDB.GetSingleObject<ParcelKindType>("ParcelKinds", Filter);

            return status;
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<ParcelKindType>("ParcelKinds", this._id, this);
        }
    }
}
