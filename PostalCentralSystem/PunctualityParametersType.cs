using DBCores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalCentralSystem
{
    public class PunctualityParametersType
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DeliveryInDays { get; set; }
        public int ShipmentHour { get; set; }
        public int ShipmentMinute { get; set; }
        public int DeliveryHour { get; set; }
        public int DeliveryMinute { get; set; }
        public bool isDefault { get; set; }


        public PunctualityParametersType()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public DateTime DeliveryPlanTime(DateTime ShipmentStartTime)
        {
            DateTime DeliveryTime;

            DeliveryTime = ShipmentStartTime.AddDays(this.DeliveryInDays);
            DateTime cutoffTime = DateTime.Parse(ShipmentStartTime.ToShortDateString() + " " + ShipmentHour + ":" + ShipmentMinute);
            if(ShipmentStartTime > cutoffTime)
            {
                DeliveryTime.AddDays(1);
            }

            DeliveryTime = DateTime.Parse(DeliveryTime.ToShortDateString() + " " + DeliveryHour + ":" + DeliveryMinute);

            return DeliveryTime;
        }


        public static PunctualityParametersType GetPunctuality(string StatusCode)
        {
            PunctualityParametersType status = new PunctualityParametersType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("Code", StatusCode);
            status = MDB.GetSingleObject<PunctualityParametersType>("PuntualictyParameters", Filter);

            return status;
        }

        public static PunctualityParametersType GetDefault()
        {
            PunctualityParametersType status = new PunctualityParametersType();

            MongoDbCore MDB = new MongoDbCore();
            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("isDefault", true);
            status = MDB.GetSingleObject<PunctualityParametersType>("PuntualictyParameters", Filter);

            return status;
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<PunctualityParametersType>("PuntualictyParameters", this._id, this);
        }


    }
}
