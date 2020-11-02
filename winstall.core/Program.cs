using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Principal;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace winstall.core
{

    public class WinPkg
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string PackageId { get; set; } = Guid.NewGuid().ToString();
        [JsonPropertyName("Id")]
        public string  Pub { get; set; }
        public string  Version { get; set; }
        public string  Name { get; set; }
        public string Publisher { get; set; }
        public string AppMoniker { get; set; }
        //public Tag Tags { get; set; }
        public string License { get; set; }
        public string  Homepage { get; set; }
        public string LicenseUrl { get; set; }
        public string Description { get; set; }
        //public InstallData Installers { get; set; }

    }

    public class InstallData
    {
        public string Arch { get; set; }
        public string Url { get; set; }
        public string InstallerType { get; set; }
        public string Sha256 { get; set; }
    }

    public class Tag
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("What would you like to enter for MyDatas?");
            //// Get some input from user
            //var myDatas = Console.ReadLine();
            //// Create an object with the data that was entered
            //var obj = new MyHelloWorldMongoThing()
            //{
            //    MyDatas = myDatas
            //};

            var mongo = new MongoClient("mongodb://localhost:27017/");
            var db = mongo.GetDatabase("winstall");

            //string[] allfiles = Directory.GetFiles(@"E:\winstall.api\winget-pkgs\manifests", "*.yaml", SearchOption.AllDirectories);
            string[] allfiles = Directory.GetFiles(@"C:\code\packages\manifests", "*.yaml", SearchOption.AllDirectories);
            //string[] allfiles = Directory.GetFiles(@"C:\code\winget - pkgs\manifests", "*.yaml", SearchOption.AllDirectories);



            // get a collection of MyHelloWorldMongoThings (and create if it doesn't exist)
            // Using an empty filter so that everything is considered in the filter.
            var collection = db.GetCollection<WinPkg>("packages");

            var test = db.ListCollections();
            // Count the items in the collection prior to insert
            var count = collection.CountDocuments(new FilterDefinitionBuilder<WinPkg>().Empty);
            Console.WriteLine($"Number of items in the collection after insert: {count}");
            // Add the entered item to the collection
            
            collection.InsertMany(GetAllPackagesData(allfiles));
            // Count the items in the collection post insert
            count = collection.CountDocuments(new FilterDefinitionBuilder<WinPkg>().Empty);
            Console.WriteLine($"Number of items in the collection after insert: {count}");
        }

        private static IEnumerable<WinPkg> GetAllPackagesData(string[] fileList)
        {
            var dataList = new List<WinPkg>();
            foreach (var file in fileList)
            {
                using (var reader = new StreamReader(file))
                {
                    var deserializer = new Deserializer();
                    var yamlObj = deserializer.Deserialize(reader);

                    var serializer = new SerializerBuilder().JsonCompatible().Build();
                    var json = serializer.Serialize(yamlObj);

                    var data = JsonSerializer.Deserialize<WinPkg>(json);

                    
                    

                    dataList.Add(data);

                }
            }

            return dataList;

        }
        


    }
}
