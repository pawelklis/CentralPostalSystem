using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralPostalSystem
{
    class ParcelType
    {
        public string _id { get; set; }
        public string guid { get; set; }
        public string SendNumber { get; set; }
        public BusinessPoint Sender { get; set; }
        public BusinessPoint Recipient { get; set; }
        public bool IsPayed { get; set; }
        public PunctualityType Punctuality { get; set; }
        public bool IsServiceCompleted { get; set; }
        public DimensionsType Dimensions { get; set; }
        public List<ParcelServiceType> Services { get; set; }
        public SortingInformationType SortingInformation { get; set; }
        public ParcelStatusType Status { get; set; }
        public List<EventType> Events { get; set; }

        public ParcelType()
        {
            guid = Guid.NewGuid().ToString();
            Sender = new BusinessPoint();
            Recipient = new BusinessPoint();
            Punctuality = new PunctualityType();
            Dimensions = new DimensionsType();
            Services = new List<ParcelServiceType>();
            SortingInformation = new SortingInformationType();
            Status = new ParcelStatusType();
            Events = new List<EventType>();
        }

        public void AddEvent(string EventCode, DateTime EventTime)
        {
            EventType NewEvent = EventType.GetEvent("P_NAD");
            NewEvent.CreateTime = EventTime;
            this.Events.Add(NewEvent);
            NewEvent.SetParcelChanges(this);
        }

    }
}
