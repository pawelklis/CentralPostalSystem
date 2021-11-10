using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DBCores;

namespace PostalCentralSystem
{
    class EventConfig
    {
        [Key]
        public string _id { get; set; }
        public string Name { get; set; }
        public string EventCode { get; set; }
        public string ParcelStatusCode { get; set; }
        public bool IsPunctualityStart { get; set; }
        public bool IsPunctualityEnd { get; set; }

        public EventConfig()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public EventType CreateEvent()
        {
            EventType ev = new EventType();

            ev.Name = this.Name;
            ev.Code = this.EventCode;
            ev.Configuration = this;

            return ev;
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<EventConfig>("EventConfigs", this._id, this);
        }

        internal static EventConfig GetEvent(string eventCode)
        {
            MongoDbCore MDB = new MongoDbCore();

            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("EventCode", eventCode);

            EventConfig o = MDB.GetSingleObject<EventConfig>("EventConfigs", Filter);
            return o;
        }
    }
}
