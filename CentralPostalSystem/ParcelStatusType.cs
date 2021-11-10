using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralPostalSystem
{
    class ParcelStatusType
    {
        public string Code { get; set; }
        public string Name { get; set; }


        public static ParcelStatusType GetStatus(string StatusCode)
        {
            ParcelStatusType status = new ParcelStatusType();
            status.Code = "N";
            status.Name = "Nadana";
            return status;
        }
    }
}
