using DBCores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalCentralSystem
{
    class RoutingTimesType
    {
        public string _id { get; set; }
        public string StartOrganizationUnit { get; set; }
        public string StartPNANumber { get; set; }
        public TimeSpan TransitTime { get; set; }
        public TimeSpan ProcessingTime { get; set; }
        public string EndOrganizationUnit { get; set; }
        public string EndPNANumber { get; set; }

        public RoutingTimesType()
        {
            this._id = Guid.NewGuid().ToString();
        }
        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<RoutingTimesType>("RoutingTimesType", this._id, this);
        }

      

        public static List<RoutingTimesType> GetRoutingTimesType()
        {
            List<RoutingTimesType> status = new List<RoutingTimesType>();

            MongoDbCore MDB = new MongoDbCore();       
            status = MDB.GetObjectList<RoutingTimesType>("RoutingTimesType");

            return status;
        }

        public static List<RoutingTimesType> GetRoutingTimesTypeStart(string PNANumber)
        {
            List<RoutingTimesType> status = new List<RoutingTimesType>();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("StartPNANumber", PNANumber );
            status = MDB.GetObjectList<RoutingTimesType>("RoutingTimesType", Filter);

            return status;
        }

        public static List<RoutingTimesType> GetRoutingTimesTypeEnd(string PNANumber)
        {
            List<RoutingTimesType> status = new List<RoutingTimesType>();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("EndPNANumber", PNANumber );
            status = MDB.GetObjectList<RoutingTimesType>("RoutingTimesType",Filter );

            return status;
        }

    }
}
