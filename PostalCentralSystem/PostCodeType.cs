using DBCores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalCentralSystem
{
    public class PostCodeType
    {
        public string _id { get; set; }
        public string PostCode { get; set; }
        public string StartOrganizationUnitCode { get; set; }
        public string EndOrganizationUnitCode { get; set; }

        public PostCodeType()
        {
            this._id = Guid.NewGuid().ToString();
        }

        public int PostCodeNum()
        {
            return int.Parse(this.PostCode.Replace("-", ""));
        }

        public void Save()
        {
            MongoDbCore MDB = new MongoDbCore();
            MDB.Update<PostCodeType>("PostCode", this._id, this);
        }



        public static List<PostCodeType> GetPostCode()
        {
            List<PostCodeType> status = new List<PostCodeType>();

            MongoDbCore MDB = new MongoDbCore();
            status = MDB.GetObjectList<PostCodeType>("PostCode");

            return status;
        }

        public static PostCodeType GetPostCode(string PostCode)
        {
            PostCodeType status;
            MongoDbCore MDB = new MongoDbCore();

            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("PostCode", PostCode);

            status = MDB.GetSingleObject<PostCodeType>("PostCode",Filter);

            return status;
        }

        public static List<PostCodeType> GetPostCodebyStartOrganizationUnitCode(string StartOrganizationUnitCode)
        {
            List<PostCodeType> status = new List<PostCodeType> ();
            MongoDbCore MDB = new MongoDbCore();

            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("PostCode", StartOrganizationUnitCode);

            status = MDB.GetObjectList<PostCodeType>("PostCode",Filter);

            return status;
        }

        public static List<PostCodeType> GetPostCodebyEndOrganizationUnitCode(string EndOrganizationUnitCode)
        {
            List<PostCodeType> status = new List<PostCodeType>();
            MongoDbCore MDB = new MongoDbCore();

            Dictionary<string, object> Filter = new Dictionary<string, object>();
            Filter.Add("PostCode", EndOrganizationUnitCode);

            status = MDB.GetObjectList<PostCodeType>("PostCode",Filter);

            return status;
        }

        

    }
}
