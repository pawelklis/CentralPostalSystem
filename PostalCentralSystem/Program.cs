using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCores;

namespace PostalCentralSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitObjects();

            PostCodeType o = new PostCodeType();
            o.PostCode = "55-080";
            o.StartOrganizationUnitCode = "55080";
            o.EndOrganizationUnitCode = "55080";
            o.Save();

            o = new PostCodeType();
            o.PostCode = "50-423";
            o.StartOrganizationUnitCode = "50423";
            o.EndOrganizationUnitCode = "50423";
            o.Save();

            ParcelType parcel = new ParcelType();
            parcel.Dimensions.Weight = 423;
            parcel.Dimensions.Width = 514;
            parcel.Dimensions.Lenght = 3860;
            parcel.Dimensions.Depth = 219;
            parcel.DimensionsByCategory("XL");
            parcel.Dimensions.SetDimensionCategory();


            parcel.SetSender(new BusinessPoint("Alojzy Trombka","Wrocław", "Piękna", "60AC", "6", "50-423","71 300 400 50","500 600 700","at@mail.com","www.trombka.pl"));
            parcel.SetRecipient( new BusinessPoint("Jan Kowalski","Stróża", "Kolejowa", "10", null, "55-080",null,"200 300 400",null,null));

            parcel.ParcelKind = ParcelKindType.GetKind("P");
      

            var c = parcel.Dimensions.Cubature();

            List<OrganizationUnitType> L = RouteStepType.GetSteps(parcel.SortingInformation.ShipmentStart);
            L.AddRange(RouteStepType.GetSteps(parcel.SortingInformation.ShipmentEnd).ToArray().Reverse());

            List<OrganizationUnitType> ls = new List<OrganizationUnitType>();
            foreach (var x in L)
            {
                bool ok = true;

                foreach(var xs in ls)
                {
                    if (xs.Code == x.Code)
                    {
                        ok = false;
                    }
                }

                if (ok == true)
                {
                    ls.Add(x);
                }
            }
            

            ls.Reverse();

Console.Clear();

            DateTime opTime = parcel.Punctuality.DeliveryPlannedTime;
            Console.WriteLine("{0} {1} {2}", parcel.SortingInformation.ShipmentEnd.Name, opTime, "Doręczenie");

            opTime = DateTime.Parse(parcel.Punctuality.DeliveryPlannedTime.ToShortDateString() +" " + parcel.SortingInformation.ShipmentEnd.CutOffForDeliveryHour +":" + parcel.SortingInformation.ShipmentEnd.CutOffForDeliveryMinute);
            Console.WriteLine("{0} {1} {2}", parcel.SortingInformation.ShipmentEnd.Name, opTime, "Zakończenie opracowania w placówce doręczającej");

            opTime = opTime.AddMinutes(-parcel.SortingInformation.ShipmentEnd.ProcessingTime.TotalMinutes); //DateTime.Parse(parcel.Punctuality.DeliveryPlannedTime.ToShortDateString() + parcel.SortingInformation.ShipmentEnd.CutOffForDeliveryHour + ":" + parcel.SortingInformation.ShipmentEnd.CutOffForDeliveryMinute);
            Console.WriteLine("{0} {1} {2}", parcel.SortingInformation.ShipmentEnd.Name, opTime, "Nadejście przesyłki do opracowania w placówce doręczającej");



            foreach (var ou in ls)
            {
                if(ou.Code != parcel.SortingInformation.ShipmentEnd.Code)
                {
                    opTime = opTime.AddMinutes(-ou.TransitToParentTime.TotalMinutes);
                    OrganizationUnitType parent = parcel.SortingInformation.ShipmentEnd.GetParent();

                    DateTime opt1 = opTime;
                    opTime = opTime.AddMinutes(-parent.ProcessingTime.TotalMinutes);
                    DateTime opt2 = opTime;


                    Console.WriteLine("{0} {1} {2}", ou.Name, opt1, "Wyjście przesyłki");         
                    Console.WriteLine("{0} {1} {2}", ou.Name, opt2, "Wejście przesyłki");   

                }           
            }

            


        }

     public static void InitObjects()
        {
            OrganizationUnitType o = new OrganizationUnitType();
            o.Name = "WER Wrocław";
            o.Code = "50900";
            o.ParentCode = "";
            o.ProcessingTime = TimeSpan.FromMinutes(45);
            o.TransitToParentTime = TimeSpan.FromMinutes(0);
            o.Save();


            o = new OrganizationUnitType();
            o.Name = "UP Kąty Wrocławskie";
            o.Code = "55080";
            o.ParentCode = "50900";
            o.CutOffForDeliveryHour = 8;
            o.CutOffForDeliveryMinute = 0;
            o.ProcessingTime = TimeSpan.FromMinutes(25);
            o.TransitToParentTime = TimeSpan.FromMinutes(50);
            o.Save();

            o = new OrganizationUnitType();
            o.Name = "Wrocław 15";
            o.Code = "50423";
            o.ParentCode = "50900";
            o.ProcessingTime = TimeSpan.FromMinutes(75);
            o.TransitToParentTime = TimeSpan.FromMinutes(34);
            o.Save();


            //RoutingTimesType o = new RoutingTimesType();
            //o.StartOrganizationUnit = "50423";
            //o.ProcessingTime = TimeSpan.FromSeconds(0);
            //o.TransitTime = TimeSpan.FromMinutes(45);
            //o.EndOrganizationUnit = "50900";
            //o.Save();



            //o = new RoutingTimesType();
            //o.StartOrganizationUnit = "50900";
            //o.ProcessingTime = TimeSpan.FromSeconds(4);
            //o.TransitTime = TimeSpan.FromMinutes(45);
            //o.EndOrganizationUnit = "55081";
            //o.Save();

            //PunctualityParametersType o = new PunctualityParametersType();
            //o.Name = "D124-24";
            //o.Code = "D1";
            //o.ShipmentHour = 23;
            //o.ShipmentMinute = 59;
            //o.DeliveryHour = 23;
            //o.DeliveryMinute = 59;
            //o.Save();

            //o = new PunctualityParametersType();
            //o.Name = "D115-24";
            //o.Code = "D1";
            //o.ShipmentHour = 15;
            //o.ShipmentMinute = 00;
            //o.DeliveryHour = 23;
            //o.DeliveryMinute = 59;
            //o.Save();

            //o = new PunctualityParametersType();
            //o.Name = "D115-12";
            //o.Code = "D1";
            //o.ShipmentHour = 15;
            //o.ShipmentMinute = 00;
            //o.DeliveryHour = 12;
            //o.DeliveryMinute = 00;
            //o.Save();

            //ParcelKindType o = new ParcelKindType();
            //o.Name = "Paczka";
            //o.Code = "P";
            //o.isDefault = true;
            //o.Punctuality = PunctualityParametersType.GetDefault();
            //o.Save();

            //o = new ParcelKindType();
            //o.Name = "Przesyłka Kurierska";
            //o.Code = "PK";
            //o.isDefault = false;
            //o.Punctuality = PunctualityParametersType.GetPunctuality("D1");
            //o.Save();

            //o = new ParcelKindType();
            //o.Name = "Przesyłka Listowa";
            //o.Code = "PL";
            //o.isDefault = false;
            //o.Punctuality = PunctualityParametersType.GetPunctuality("D115");
            //o.Save();

            //o = new ParcelKindType();
            //o.Name = "Przesyłka Paletowa";
            //o.Code = "PP";
            //o.isDefault = false;
            //o.Punctuality = PunctualityParametersType.GetPunctuality("D11512");
            //o.Save();

            //EventConfig o = new EventConfig();

            //o.EventCode = "P";
            //o.IsPunctualityEnd = false;
            //o.IsPunctualityStart = true;
            //o.Name = "Przygotowanie przesyłki";
            //o.ParcelStatusCode = "P";
            //o.Save();


            //ParcelStatusType o = new ParcelStatusType();
            //o.Name = "P";
            //o.Code = "Przygotowana";
            //o.DeafaultStatus = true;
            //o.Save();

            //o = new ParcelStatusType();
            //o.Name = "Dostarczona";
            //o.Code = "D";
            //o.Save();

            //DimmensionCategoryType o = new DimmensionCategoryType();
            //o.Code = "S";
            //o.MaxDepth = 500;
            //o.MaxLenght = 500;
            //o.MaxWeight = 500;
            //o.MaxWidth = 500;

            //o.MinDepth = 0;
            //o.MinLenght = 0;
            //o.MinWeight = 0;
            //o.MinWidth = 0;

            //o.Save();

            //o = new DimmensionCategoryType();
            //o.Code = "M";
            //o.MaxDepth = 1000;
            //o.MaxLenght = 1000;
            //o.MaxWeight = 1000;
            //o.MaxWidth = 1000;

            //o.MinDepth = 500;
            //o.MinLenght = 500;
            //o.MinWeight = 500;
            //o.MinWidth = 500;

            //o.Save();

            //o = new DimmensionCategoryType();
            //o.Code = "XL";
            //o.MaxDepth = 9000;
            //o.MaxLenght = 9000;
            //o.MaxWeight = 9000;
            //o.MaxWidth = 9000;

            //o.MinDepth = 1000;
            //o.MinLenght = 1000;
            //o.MinWeight = 1000;
            //o.MinWidth = 1000;

            //o.Save();
        }

    }
}
