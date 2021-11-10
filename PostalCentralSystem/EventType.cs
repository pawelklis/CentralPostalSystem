using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    class EventType
    {
        [Key]
        public string _id { get; set; }
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

                Parcel.Punctuality.DeliveryPlannedTime  = Parcel.ParcelKind.Punctuality.DeliveryPlanTime(this.CreateTime);
            }

            if (this.Configuration.IsPunctualityEnd == true)
            {
                Parcel.Punctuality.DeliveryPlannedTime = this.CreateTime;
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

            EventConfig ec = EventConfig.GetEvent(EventCode);
                 

            _Event = ec.CreateEvent();


            return _Event;
        }

    }
}
