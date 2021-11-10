using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    class EmployeeType: OrganizationUnitType
    {
        [Key]
        public string _id { get; set; }
        public string Surname { get; set; }

    }
}
