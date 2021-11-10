using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PostalCentralSystem
{
    public class AdressType
    {
        [Key]
        public string _id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Local { get; set; }
        public PostCodeType PostCode { get; set; }





        public AdressType()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public AdressType(string city = null, string street=null,string house=null, string local=null,string postcode=null)
        {
            this._id = Guid.NewGuid().ToString();
            this.City = city;
            this.Street = street;
            this.House = house;
            this.Local = local;
            this.PostCode = PostCodeType.GetPostCode(postcode);
        }



    }
}
