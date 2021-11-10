using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PostalCentralSystem
{
    public class ContactType
    {
        [Key]
        public string _id { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }


        public ContactType()
        {
            this._id = Guid.NewGuid().ToString();
        }
        public ContactType(string phone=null,string mobilephone=null,string email=null,string website=null)
        {
            this._id = Guid.NewGuid().ToString();
            this.Phone = phone;
            this.MobilePhone = mobilephone;
            this.Email = email;
            this.WebSite = website;


        }

    }
}
