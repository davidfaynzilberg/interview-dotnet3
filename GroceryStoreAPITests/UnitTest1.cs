using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace GroceryStoreAPITests
{
    [TestClass]
    public class UnitTest1
    {
        private const string ExpectedMethod1 = "Bob";
        [TestMethod]
        public void GetCustomerByIdTestMethod1()
        {
            var result = "";

            CustomerController customerController = new CustomerController();
            if( customerController.UpdateCustomer(1, ExpectedMethod1) )
                result = customerController.GetCustomerById(1);

            Assert.AreEqual("Id: 1 - Name: " + ExpectedMethod1, result);
        }

        private const string ExpectedMethod2 = "";
        [TestMethod]
        public void GetCustomerByIdTestMethod2()
        {
            CustomerController customerController = new CustomerController();
            var result = customerController.GetCustomerById(4);

            Assert.AreNotEqual(ExpectedMethod2, result);
        }

        private const string ExpectedMethod3 = "";
        [TestMethod]
        public void GetCustomerByIdTestMethod3()
        {
            CustomerController customerController = new CustomerController();
            var result = customerController.GetCustomerById(null);

            Assert.AreEqual(ExpectedMethod3, result);
        }

        private const int ExpectedMethod4 = 0;
        [TestMethod]
        public void GetAllCustomersTestMethod3()
        {
            CustomerController customerController = new CustomerController();
            List<Customer> result = customerController.GetAllCustomers();

            Assert.IsTrue(result.Count > ExpectedMethod4);
        }

        [TestMethod]
        public void AddRecordToJsonFileTestMethod()
        {
            string ExpectedMethod5 = RandName();
            CustomerController customerController = new CustomerController();
            bool result = customerController.AddCustomer(ExpectedMethod5);

            Assert.AreEqual(true, result);

        }
        
        [TestMethod]
        public void UpdateRecordToJsonFileTestMethod()
        {
            string ExpectedMethod7 = RandName();
            CustomerController customerController = new CustomerController();
            bool result = customerController.UpdateCustomer(1, ExpectedMethod7);

            Assert.AreEqual(true, result);

        }

        private const string ExpectedMethod6 = "David Faynzilberg";
        [TestMethod]
        public void DuplicateInsertTestMethod()
        {
            CustomerController customerController = new CustomerController();
            
            bool result = customerController.AddCustomer(ExpectedMethod6);
            
            if(result)
            {
                result = customerController.AddCustomer(ExpectedMethod6);
            }

            Assert.AreEqual(false, result);

        }
        private string RandName()
        {
            string firstName, lastName = "";

            String[] maleNames = new String[] { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
            String[] femaleNames = new String[] { "abby", "abigail", "adele", "adrian" };
            String[] lastNames = new String[] { "abbott", "acosta", "adams", "adkins", "aguilar" };

            Random rand = new Random(DateTime.Now.Second);
            if (rand.Next(1, 2) == 1)
            {
                firstName = maleNames[rand.Next(0, maleNames.Length - 1)];
            }
            else
            {
                firstName = femaleNames[rand.Next(0, femaleNames.Length - 1)];
            }

            rand = new Random(DateTime.Now.Second);
            lastName = lastNames[rand.Next(0, lastNames.Length - 1)];

            return firstName + " " + lastName;
        }
    }
}
