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
        private const string ExpectedMethod1 = "Id: 1 - Name: Bob";
        [TestMethod]
        public void GetCustomerByIdTestMethod1()
        {
            CustomerController customerController = new CustomerController();
            var result = customerController.GetCustomerById(1);

            Assert.AreEqual(ExpectedMethod1, result);
        }

        private const string ExpectedMethod2 = "";
        [TestMethod]
        public void GetCustomerByIdTestMethod2()
        {
            CustomerController customerController = new CustomerController();
            var result = customerController.GetCustomerById(4);

            Assert.AreEqual(ExpectedMethod2, result);
        }

        private const string ExpectedMethod3 = "";
        [TestMethod]
        public void GetCustomerByIdTestMethod3()
        {
            CustomerController customerController = new CustomerController();
            var result = customerController.GetCustomerById(null);

            Assert.AreEqual(ExpectedMethod3, result);
        }

        private const int ExpectedMethod4 = 3;
        [TestMethod]
        public void GetAllCustomersTestMethod3()
        {
            CustomerController customerController = new CustomerController();
            List<Customer> result = customerController.GetAllCustomers();

            Assert.AreEqual(ExpectedMethod4, result.Count);
        }

        private const string ExpectedMethod5 = "David Faynzilberg";
        [TestMethod]
        public void AddRecordToJsonFileTestMethod()
        {
            CustomerController customerController = new CustomerController();
            bool result = customerController.AddCustomer(ExpectedMethod5);

            Assert.AreEqual(true, result);

        }
    }
}
