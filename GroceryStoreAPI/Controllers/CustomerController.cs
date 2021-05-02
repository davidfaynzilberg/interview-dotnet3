using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public class CustomerController : ControllerBase
    {
        [HttpPatch]
        [Route("/[controller]/[action]/{id}/{name}")]
        // TODO: impliment security
        // API adding a customer
        public bool UpdateCustomer(int id, string name)
        {
            bool returnSuccessfulInsertFlag = true;

            if (name == "")
                returnSuccessfulInsertFlag = false;

            returnSuccessfulInsertFlag = new Utils.UtilClass().UpdateRecordToJsonFile(id, name);

            return returnSuccessfulInsertFlag;
        }

        [HttpPost]
        [Route("/[controller]/[action]/{name}")]
        // TODO: impliment security
        // API adding a customer
        public bool AddCustomer(string name)
        {
            bool returnSuccessfulInsertFlag = true;

            if (name == "")
                returnSuccessfulInsertFlag = false;

               returnSuccessfulInsertFlag = new Utils.UtilClass().AddRecordToJsonFile(name);

            return returnSuccessfulInsertFlag;
        }
        
        [HttpGet]
        [Route("/[controller]/[action]/{name}")]
        // TODO: impliment security
        public string GetCustomerIdByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "";

            // API retrieving a customer Id by name
            JObject jsonData = new Utils.UtilClass().ReadJsonFile();

            IEnumerable<JToken> customerId = jsonData.SelectToken("$.customers[?(@.name == '" + name + "')].id");

            return customerId != null ? string.Format("Id: {0} - Name: {1}", customerId, name) : "Not Found";
        }

        [HttpGet]
        [Route("/[controller]/[action]/{id}")]
        // TODO: impliment security
        public string GetCustomerById(int? id)
        {
            if (!id.HasValue)
                return "";

            // API retrieving a customer name by Id
            JObject jsonData = new Utils.UtilClass().ReadJsonFile();

            IEnumerable<JToken> customerName = jsonData.SelectToken("$.customers[?(@.id == " + id + ")].name");

            return customerName != null ? string.Format("Id: {0} - Name: {1}", id, customerName) : "";
        }

        [HttpGet]
        [Route("/[controller]/[action]")]
        // TODO: impliment security
        public List<Customer> GetAllCustomers()
        {
            // API listing all customers
            // Return in Json List<Customer> format
            // string returnStringList = "";

            JObject jsonData = new Utils.UtilClass().ReadJsonFile();
            JArray allCustomers = (JArray)jsonData["customers"];

            // Return in string format
            //foreach (JToken customer in allCustomers)
            //{
            //    returnStringList += string.Format("Id: {0} - Name: {1} \n", customer["id"], customer["name"]);
            //}
            //return returnStringList.Substring(0, returnStringList.Length - 1); // removing last new line

            return allCustomers.ToObject<List<Customer>>();
        }
    }

    //public class CustomerController : Controller
    //{
    //    // GET: CustomerController
    //    // get List of all Customers
    //    [HttpGet]
    //    //[ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
    //    //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    //    public IEnumerable<Customer> Get()
    //    {
    //        return new List<Customer>();
    //    }

    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    // GET: CustomerController/Details/5
    //    public ActionResult Details(int id)
    //    {
    //        return View();
    //    }

    //    // GET: CustomerController/Create
    //    public ActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: CustomerController/Create
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create(IFormCollection collection)
    //    {
    //        try
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: CustomerController/Edit/5
    //    public ActionResult Edit(int id)
    //    {
    //        return View();
    //    }

    //    // POST: CustomerController/Edit/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }

    //    // GET: CustomerController/Delete/5
    //    public ActionResult Delete(int id)
    //    {
    //        return View();
    //    }

    //    // POST: CustomerController/Delete/5
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Delete(int id, IFormCollection collection)
    //    {
    //        try
    //        {
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    //}
}
