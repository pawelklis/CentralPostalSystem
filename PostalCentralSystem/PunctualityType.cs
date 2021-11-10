using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    class PunctualityType
    {
        [Key]
        public string _id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime SipmentPlannedTime { get; set; }
        public DateTime ShipmentStartTime { get; set; }
        public DateTime ShipmentEndTime { get; set; }
        public DateTime DeliveryPlannedTime { get; set; }


      

    }
}
