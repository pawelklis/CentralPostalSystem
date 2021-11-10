using DBCores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PostalCentralSystem
{
    class ParcelType
    {
        [Key]
        public string _id { get; set; }        
        public string SendNumber { get; set; }
        public ParcelKindType ParcelKind { get; set; }
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

        public void SetSender(BusinessPoint sender)
        {
            this.Sender = sender;
            this.SetSortingInformation();
        }
        public void SetRecipient(BusinessPoint recipient)
        {
            this.Recipient = recipient;
            this.SetSortingInformation();
        }

        public void DimensionsByCategory(string DimensionCategoryCode)
        {
            DimmensionCategoryType dm = DimmensionCategoryType.GetDimensionCategory(DimensionCategoryCode);

            this.Dimensions.Weight = dm.MaxWeight;
            this.Dimensions.Width = dm.MaxWidth;
            this.Dimensions.Lenght = dm.MaxLenght;
            this.Dimensions.Depth = dm.MaxDepth;

            this.Dimensions.DimensionCategory = dm;
        }


        public ParcelType()
        {
            _id = Guid.NewGuid().ToString();
            
            Sender = new BusinessPoint();
            Recipient = new BusinessPoint();
            Punctuality = new PunctualityType();
            Dimensions = new DimensionsType();
            Services = new List<ParcelServiceType>();            
            Status = new ParcelStatusType();
            Events = new List<EventType>();

            Punctuality.CreateTime = DateTime.Now;
            Status = ParcelStatusType.GetDefault();

            ParcelKind = ParcelKindType.GetDefault();
            Punctuality.SipmentPlannedTime = Punctuality.CreateTime; //domyslny planowany czas nadania wg rodzaju przesylki
            Punctuality.DeliveryPlannedTime = ParcelKind.Punctuality.DeliveryPlanTime(Punctuality.SipmentPlannedTime);

            AddEvent("P", DateTime.Now);
                       
        }

        private void SetSortingInformation()
        {
            SortingInformation = new SortingInformationType(Sender.Address?.PostCode?.PostCode, Recipient.Address?.PostCode?.PostCode);
        }

        public void AddEvent(string EventCode, DateTime EventTime)
        {
            EventType NewEvent = EventType.GetEvent(EventCode);
            NewEvent.CreateTime = EventTime;
            this.Events.Add(NewEvent);
            NewEvent.SetParcelChanges(this);
            this.Save();
        }


        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<ParcelType>("parcels", this._id,this);
        }

    }
}
