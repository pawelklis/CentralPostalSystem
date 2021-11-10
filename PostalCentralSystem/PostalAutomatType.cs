using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    class PostalAutomatType: OrganizationUnitType
    {
        [Key]
        public string _id { get; set; }
        public string BoxNumber { get; set; }
        public int BoxID { get; set; }

    }
}
