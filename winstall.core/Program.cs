using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Principal;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace winstall.core
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongo = new MongoClient("mongodb://localhost:27017/");
            var db = mongo.GetDatabase("winstall");

            //string[] allfiles = Directory.GetFiles(@"E:\winstall.api\winget-pkgs\manifests", "*.yaml", SearchOption.AllDirectories);
            string[] allfiles = Directory.GetFiles(@"C:\code\packages\manifests", "*.yaml", SearchOption.AllDirectories);
            var collection = db.GetCollection<WinPkg>("packages");

            var test = db.ListCollections();
            var count = collection.CountDocuments(new FilterDefinitionBuilder<WinPkg>().Empty);
            Console.WriteLine($"Number of items in the collection after insert: {count}");
            
            collection.InsertMany(GetAllPackagesData(allfiles));
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
