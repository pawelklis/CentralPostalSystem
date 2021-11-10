using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CentralPostalSystem
{
    class Program
    {


        public class BlogDBContext : DBContext
        {
            public DbSet<AdressType> AdressTypes { get; set; }
        }
        static void Main(string[] args)
        {
            OrganizationUnitType oa = new OrganizationUnitType();
            oa.Name = "Urząd pocztowy";

            EmployeeType oe = new EmployeeType();
            oe.Surname = "Kowalski";

            SortingInformationType si = new SortingInformationType();
            si.ShipmentStart = oa;
            si.ShipmentEnd = oe;


            ParcelType Paczka = new ParcelType();

            Paczka.AddEvent("P_NAD",DateTime.Now);

            Console.ReadLine();
        }

        static void pytaniaRekr()
        {

            int ileliczb = 45;
            int poprzednialiczba = -1;
            int nastepnaliczba = 1;

            for (int i = 0; i < ileliczb; i++)
            {
                int suma = poprzednialiczba + nastepnaliczba;
                poprzednialiczba = nastepnaliczba;
                nastepnaliczba = suma;




                Console.Write(nastepnaliczba + "|");
            }


            for (int i = 0; i < 100; i++)
            {
                if (i % 15 == 0)
                {
                    Console.WriteLine(i);
                }
                else if (i % 5 == 0)
                {
                    {
                        Console.WriteLine(i);
                    }

                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine(i);
                }

            }

            List<String> names = new List<String>();
            names.Add("Bruce");
            names.Add("Alfred");
            names.Add("Tim");
            names.Add("Richard");


            names.ForEach(delegate (String name)
            {
                Console.WriteLine(name);
            });
        }
        public static int fib(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            return fib(n - 2) + fib(n - 1);
        }



    }
}
