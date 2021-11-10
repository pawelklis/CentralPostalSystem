using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DBCores;

namespace PostalCentralSystem
{
    class ParcelStatusType
    {
        [Key]
        public string _id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool DeafaultStatus { get; set; }

        public ParcelStatusType()
        {
            this._id = Guid.NewGuid().ToString();
        }
        public static ParcelStatusType GetStatus(string StatusCode)
        {
            ParcelStatusType status = new ParcelStatusType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("Code", StatusCode);
            status = MDB.GetSingleObject<ParcelStatusType>("Parcelstatuses", Filter);

            return status;
        }

        public static ParcelStatusType GetDefault()
        {
            ParcelStatusType status = new ParcelStatusType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("DeafaultStatus", true);
            status = MDB.GetSingleObject<ParcelStatusType>("Parcelstatuses", Filter);

            return status;
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<ParcelStatusType>("Parcelstatuses", this._id, this);
        }
    }
}
