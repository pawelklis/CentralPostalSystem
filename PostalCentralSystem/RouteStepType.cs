using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalCentralSystem
{
    class RouteStepType
    {

        public RoutingTimesType Step { get; set; }
        public DateTime StepStartTime { get; set; }


        public static List<OrganizationUnitType> GetSteps(OrganizationUnitType startOrg)
        {
            List<OrganizationUnitType> l = new List<OrganizationUnitType>();
            l.Add(startOrg);
            int i = 0;
            OrganizationUnitType nextParent;
            do
            {
                nextParent = l[i].GetParent();
                if(nextParent.Code!=null)
                    l.Add(nextParent);
                i++;
            } while (nextParent.ParentCode!=null);

            return l;
        }


    }
}
