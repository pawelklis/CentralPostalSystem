using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    class ParcelServiceType
    {
        [Key]
        public string _id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }       
        public string ServiceForParcelCode { get; set; }

    }
}
