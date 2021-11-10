using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    public class SortingInformationType
    {
        [Key]
        public string _id { get; set; }
        public OrganizationUnitType ShipmentStart { get; set; }
        public OrganizationUnitType ShipmentEnd { get; set; }


        public SortingInformationType(string startPostCode, string EndPostCode)
        {
            if (startPostCode != null)
            { 
            PostCodeType startPC = PostCodeType.GetPostCodebyStartOrganizationUnitCode(startPostCode)[0];
            ShipmentStart = OrganizationUnitType.GetOrganizationUnit(startPC.StartOrganizationUnitCode);
            }
            if (EndPostCode != null)
            {
            PostCodeType endPC = PostCodeType.GetPostCodebyEndOrganizationUnitCode(EndPostCode)[0];
            ShipmentEnd = OrganizationUnitType.GetOrganizationUnit(endPC.EndOrganizationUnitCode);
            }

        }

    }
}
