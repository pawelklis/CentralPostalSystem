using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DBCores;

namespace PostalCentralSystem
{
    public class OrganizationUnitType
    {
        [Key]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public BusinessPoint Address { get; set; }

        public int CutOffForDeliveryHour { get; set; }
        public int CutOffForDeliveryMinute { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public TimeSpan TransitToParentTime { get; set; }

        public OrganizationUnitType()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<OrganizationUnitType>("OrganizationUnits", this._id, this);
        }

        public static List<OrganizationUnitType> GetOrganizationUnit()
        {
            List<OrganizationUnitType> status = new List<OrganizationUnitType>();

            MongoDbCore MDB = new MongoDbCore();       
            status = MDB.GetObjectList<OrganizationUnitType>("OrganizationUnits");

            return status;
        }

        public static OrganizationUnitType GetOrganizationUnit(string code)
        {
            OrganizationUnitType status = new OrganizationUnitType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("Code", code);
            status = MDB.GetSingleObject<OrganizationUnitType>("OrganizationUnits", Filter);

            return status;
        }

        public OrganizationUnitType GetParent()
        {
            OrganizationUnitType Parent = new OrganizationUnitType();
            Parent = OrganizationUnitType.GetOrganizationUnit(this.ParentCode);
            return Parent;
        }

    }
}
