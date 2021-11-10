using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralPostalSystem
{
    class EventConfig
    {
        public string _id { get; set; }
        public string Name { get; set; }
        public string EventCode { get; set; }
        public string ParcelStatusCode { get; set; }
        public bool IsPunctualityStart { get; set; }
        public bool IsPunctualityEnd { get; set; }



        public EventType CreateEvent()
        {
            EventType ev = new EventType();

            ev.Name = this.Name;
            ev.Code = this.EventCode;
            ev.Configuration = this;

            return ev;
        }

    }
}
