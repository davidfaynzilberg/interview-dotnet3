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
        
        public bool UpdateRecordToJsonFile(int id, string newName)
        {
            bool successfulInsertFlag = false;

            // API retrieving a customer name by Id
            JObject jsonData = ReadJsonFile();

            IEnumerable<JToken> customerId = jsonData.SelectToken("$.customers[?(@.id == " + id + ")].name");

            if(customerId != null)
            {   // if Customer found getting array from JSON
                List<Customer> allCustomers = jsonData["customers"].ToObject<List<Customer>>();

                Customer objCustomer = allCustomers.FirstOrDefault(x => x.id == id);
                
                if (objCustomer != null) 
                    objCustomer.name = newName;

                string jsonOutput = JsonConvert.SerializeObject(new
                {
                    customers = allCustomers
                });

                // Write out the file
                File.WriteAllText(FILE_PATH, jsonOutput);

                // TODO: check successful insert

                successfulInsertFlag = true;
            }

            return successfulInsertFlag;
        }

        public bool AddRecordToJsonFile(string name)
        {
            bool successfulInsertFlag = true;
            
            JObject jsonData = ReadJsonFile();
            List<Customer> allCustomers = jsonData["customers"].ToObject<List<Customer>>();

            // check if name exists
            if (allCustomers.Any(a => a.name == name))
            {
                Console.WriteLine("Name exists!");
                return false;
            }

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
}
