using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentralPostalSystem
{
    class PostalAutomatType: OrganizationUnitType
    {
        public string BoxNumber { get; set; }
        public int BoxID { get; set; }

    }
}
