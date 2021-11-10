using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    public class BusinessPoint
    {
        [Key]
        public string _id { get; set; }
        public string Name { get; set; }
        public AdressType Address { get; set; }
        public ContactType Contact { get; set; }

        public BusinessPoint()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public BusinessPoint(string name=null,
            string city = null, string street = null, string house = null, string local = null, string postcode = null,
            string phone = null, string mobilephone = null, string email = null, string website = null)
        {
            this._id = Guid.NewGuid().ToString();
            this.Address = new AdressType(city, street, house, local, postcode);
            this.Contact = new ContactType(phone, mobilephone, email, website);
            this.Name = name;
        }



    }
}
