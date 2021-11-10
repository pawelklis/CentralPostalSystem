using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;

namespace DBCores
{


public class MongoDbCore
{
    /// <summary>
    ///         ''' "mongodb://localhost/"
    ///         ''' </summary>
    ///         ''' <returns></returns>
    public string ConnectionString { get; set; } = "mongodb://localhost/";
    public string DBName { get; set; } = "PCS";
    public MongoDatabase DataBase { get; set; }

    public MongoDbCore( string DBName = "PCS", string ConnectionString = "mongodb://localhost/")
    {
        this.ConnectionString = ConnectionString;
        this.DBName = DBName;

        this.DataBase = this.Connect();
    }

    private MongoDatabase Connect()
    {
        MongoServer server;
        MongoClient client;
        MongoDatabase db;

        client = new MongoClient(this.ConnectionString);
        db = client.GetServer().GetDatabase(this.DBName);
        return db;
    }





    public List<string> GetDistinct(string TableName, string FieldName)
    {
        var coll = this.DataBase.GetCollection(TableName);
        var bl = coll.Distinct(FieldName).ToList();
        List<string> l = new List<string>();

        foreach (var b in bl)
            l.Add(b.ToString());

        return l;
    }
    public T GetSingleObject<T>(string TableName, Dictionary<string, object> Filters = null) where T : new()
    {
        IMongoQuery qf=Query.Null;
        List<IMongoQuery> queries = new List<IMongoQuery>();
        if (Filters!=null)
        {
            foreach (var f in Filters)
            {
                IMongoQuery qu;
                var b = BsonValue.Create(f.Value).AsBsonValue;
                if (IsNumeric(b.ToString()))
                    qu = Query.EQ(f.Key, b);
                else
                    qu = Query.EQ(f.Key, b);

                queries.Add(qu);
            }

            qf = Query.And(queries.ToArray());
        }



        var coll = this.DataBase.GetCollection(TableName);


        List<BsonDocument> result;



        if (Filters==null)
            result = coll.FindAll().ToList();
        else
            result = coll.Find(qf).ToList();


        T x=new T();
        if (result.Count > 0)
            x = BsonToObject<T>(result[0]);
        return x;
    }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        public static bool IsNumerics(string value)
        {
            return value.All(char.IsNumber);
        }
        public List<T> GetObjectList<T>(string TableName, Dictionary<string, object> Filters = null) where T : new()
    {
            IMongoQuery qf = Query.Null;
            List<IMongoQuery> queries = new List<IMongoQuery>();
        if (Filters !=null)
        {
            foreach (var f in Filters)
            {
                IMongoQuery qu;
                var b = BsonValue.Create(f.Value).AsBsonValue;
                if (IsNumeric(b.ToString()))
                    qu = Query.EQ(f.Key, b);
                else
                    qu = Query.EQ(f.Key, b);

                queries.Add(qu);
            }

            qf = Query.And(queries.ToArray());
        }







        var coll = this.DataBase.GetCollection(TableName);


            List<BsonDocument> result;


            if (Filters==null)
            result = coll.FindAll().ToList();
        else
            result = coll.Find(qf).ToList();

        List<T> l = new List<T>();
        foreach (var o in result)
            l.Add(BsonToObject<T>(o));

        return l;
    }
    public void Insert<T>(string TableName, object ob) where T : new()
    {
        var coll = this.DataBase.GetCollection(TableName);

        string JsonString = JsonConvert.SerializeObject(ob);

        BsonDocument bs = BsonDocument.Parse(JsonString);


        coll.Insert(bs);
    }

    public void InsertList<T>(List<object> l, string TableName) where T : new()
    {
        var coll = this.DataBase.GetCollection(TableName);

        var mongoClient = new MongoClient(ConnectionString);
        var db = mongoClient.GetDatabase(this.DataBase.Name);

        var products = this.DataBase.GetCollection(TableName);

        using (var session = mongoClient.StartSession())
        {
            try
            {
                session.StartTransaction();

                foreach (var o in l)
                {
                    string JsonString = JsonConvert.SerializeObject(o);

                    BsonDocument bs = BsonDocument.Parse(JsonString);

                    coll.Insert(bs);
                }



                session.CommitTransaction();
            }
            catch (Exception __unusedException1__)
            {
                session.AbortTransaction();
            }
        }
    }

    public void Delete<T>(string TableName, string ob_id) where T : new()
    {
        var coll = this.DataBase.GetCollection(TableName);

        string id = ob_id;

        var q = new QueryDocument("_id", id);
        coll.Remove(q);
    }
    public static void Delete(MongoCollection coll, string id)
    {
        if (id !=null)
        {
            var q = new QueryDocument("_id", id);
            coll.Remove(q);
        }
    }

    public static CommandResult drop(string tablename)
    {
        MongoDbCore m = new MongoDbCore();
        var coll = m.DataBase.GetCollection(tablename);
        return coll.Drop();
    }
    public static string Delete(string TableName, Dictionary<string, object> filters)
    {
            MongoDbCore m = new MongoDbCore();
        var coll = m.DataBase.GetCollection(TableName);



            IMongoQuery qf = Query.And(null);
            List<IMongoQuery> queries = new List<IMongoQuery>();
        if (filters !=null)
        {
            foreach (var f in filters)
            {
                IMongoQuery qu;
                var b = BsonValue.Create(f.Value).AsBsonValue;
                if (IsNumerics(b.ToString()))
                    qu = Query.EQ(f.Key, b);
                else
                    qu = Query.EQ(f.Key, b);

                queries.Add(qu);
            }

            qf = Query.And(queries.ToArray());
        }

        WriteConcernResult a = coll.Remove(qf);
        return a.DocumentsAffected.ToString();
    }
    public void Update<T>(string TableName, string _id, object ob) where T : new()
    {
        var coll = this.DataBase.GetCollection(TableName);

        string JsonString = JsonConvert.SerializeObject(ob);

        BsonDocument bs = BsonDocument.Parse(JsonString);

        Delete(coll, _id);

        coll.Save(bs);
    }



    class testclas
    {
        public string _id { get; set; }
        public string Dzień { get; set; }

        public string Identyfikator_przesyłki { get; set; }

        public string Kod__jednostki { get; set; }

        public string Nazwa_jednostki { get; set; }

        public string Region { get; set; }

        public string Nazwa_fazy { get; set; }
    }

    public BsonDocument ObjectToBson<T>(object ob) where T : new()
    {
        Dictionary<string, object> ds = new Dictionary<string, object>();

        var Props = typeof(T).GetProperties();
        foreach (var p in Props)
        {
            if (p.Name.ToLower() == "_id")
                ds.Add(p.Name.ToString(), p.GetValue(ob));
            else
                ds.Add(p.Name.ToString().Replace("_", " "), p.GetValue(ob));
        }

        BsonDocument bs = new BsonDocument(ds);
        return bs;
    }
    public T BsonToObject<T>(BsonDocument bs) where T : new()
    {
        var Props = typeof(T).GetProperties();
        T a = new T();
        string JsonString = bs.ToJson();
        JsonString = JsonString.Replace("ObjectId(", "").Replace("),", ",");
        a = (T)JsonConvert.DeserializeObject(JsonString, typeof(T));

        return a;
    }
}


}