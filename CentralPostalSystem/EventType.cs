using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralPostalSystem
{
    class EventType
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public OrganizationUnitType OrganizationUnit { get; set; }
        public EmployeeType Employee { get; set; }
        public PostalAutomatType Automat { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime SavedTime { get; set; }
        public string Description { get; set; }
        public EventConfig Configuration { get; set; }


        public void SetParcelChanges(ParcelType Parcel)
        {
            if (this.Configuration.IsPunctualityStart == true)
            {
                Parcel.Punctuality.ShipmentStartTime = this.CreateTime;
            }

            if (this.Configuration.IsPunctualityEnd == true)
            {
                Parcel.Punctuality.DeliveryTime = this.CreateTime;
            }
            if (string.IsNullOrEmpty(this.Configuration.ParcelStatusCode) == false)
            {
                Parcel.Status =ParcelStatusType.GetStatus( this.Configuration.ParcelStatusCode);
            }

        }

        public static EventType GetEvent(string EventCode)
        {
            EventType _Event = new EventType();


            //load event config from db

            EventConfig ec = new EventConfig();
            ec.EventCode = "P_NAD";
            ec.IsPunctualityEnd = false;
            ec.IsPunctualityStart = true;
            ec.Name = "Nadanie masowe";
            ec.ParcelStatusCode = "N";

            _Event = ec.CreateEvent();


            return _Event;
        }

    }
}
