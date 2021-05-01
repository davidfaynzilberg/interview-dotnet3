using GroceryStoreAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Utils
{
    public class UtilClass
    {
        private const string DATABASE_FILE = "database.json";
        private string FILE_PATH = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"")) + @"\" + DATABASE_FILE;

        // TODO: get connection property only once
        public JObject ReadJsonFile()
        {
            JObject jsonObject = null;
            
            // read JSON directly from a file
            using (StreamReader file = File.OpenText(FILE_PATH))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonObject = (JObject)JToken.ReadFrom(reader);
            }

            return jsonObject;
        }

        public bool AddRecordToJsonFile(string name)
        {
            bool successfulInsertFlag = true;
            
            JObject jsonData = ReadJsonFile();
            List<Customer> allCustomers = jsonData["customers"].ToObject<List<Customer>>();

            // Update json data array
            allCustomers.Add(new Customer 
            {
                id = allCustomers.Count + 1,
                name = name
            });

            // Write out the file
            string jsonOutput = JsonConvert.SerializeObject(new
            {
                customers = allCustomers
            });
            File.WriteAllText(FILE_PATH, jsonOutput);

            successfulInsertFlag = CheckIfRecordSuccessfullyInserted(allCustomers.Count + 1);
            
            if( successfulInsertFlag )
                successfulInsertFlag = CheckIfRecordSuccessfullyInserted(name);

            return successfulInsertFlag;
        }

        private bool CheckIfRecordSuccessfullyInserted(int expectedNumberOfRecords)
        {
            JObject jsonData = ReadJsonFile();
            List<Customer> allCustomers = jsonData["customers"].ToObject<List<Customer>>();

            return expectedNumberOfRecords == allCustomers.Count + 1 ? true : false;
        }
        private bool CheckIfRecordSuccessfullyInserted(string name)
        {
            JObject jsonData = ReadJsonFile();
            List<Customer> allCustomers = jsonData["customers"].ToObject<List<Customer>>();

            return allCustomers.Any(cus => cus.name == name);
        }
    }

    // TODO: for real connections
    // for multi threaded environment
    // DF 04/30/2021 not in use
    public sealed class Singleton
    {
        private static volatile Singleton instance;
        private static object syncRoot = new Object();

        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }

                return instance;
            }
        }
    }

    // TODO: connect to db
    // DF 04/30/2021 not in use
    public sealed class SingletonDB
    {
        private static readonly SingletonDB instance = new SingletonDB();
        private readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mydb"].ConnectionString);

        // Explicit static constructor to tell C# compiler
        // not to mark type as before field init
        static SingletonDB()
        {
        }

        private SingletonDB()
        {
        }

        public static SingletonDB Instance
        {
            get
            {
                return instance;
            }
        }

        public SqlConnection GetDBConnection()
        {
            return con;
        }
    }

}
