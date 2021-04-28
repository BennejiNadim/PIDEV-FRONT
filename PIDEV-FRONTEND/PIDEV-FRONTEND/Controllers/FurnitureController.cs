using PIDEV_FRONTEND.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PIDEV_FRONTEND.Controllers
{
    public class FurnitureController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:8081/";
        public async Task<ActionResult> Index()
        {
            List<Furniture> furnInfo = new List<Furniture>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/getallFurniture");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api 
                    var FrunResponse = Res.Content.ReadAsStringAsync().Result;

                    //string apiResponse = await response.Content.ReadAsStringAsync();

                    var deserializedjsonResult = Newtonsoft.Json.Linq.JObject.Parse(FurnResponse);
                    var token = Newtonsoft.Json.Linq.JObject.Parse(FurnResponse).SelectToken("data");

                    if (token.Count<object>() != 0)
                    {
                        FurnInfo = token.ToObject<List<Furniture>>();
                    }

                    //Deserializing the response recieved from web api and storing into the Employee list
                    //EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);

                }
                //returning the employee list to view
                return View(Furn);
            }
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Furniture furn)
        {
            string Baseurl = "http://localhost:8081/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");
                var response = await client.PostAsJsonAsync("api/getallFurniture", furn);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(furn);
        }

        public ActionResult Edit(int id)
        {
            Furniture furn = null;

            using (var client = new HttpClient())
            {



                // localhost:8081/api/updateFurniture?id=2

                client.BaseAddress = new Uri("http://localhost:8081/api");
                //HTTP GET
                var responseTask = client.GetAsync("updateFurniture/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Furniture>();
                    readTask.Wait();

                    furn = readTask.Result;
                }
            }
            return View(furn);
        }


        //
        // POST: /Products/Edit/5

        [HttpPost]
        public ActionResult Edit(Furniture furn)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Furniture>("furniture", furn);               // was student

                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(furn);
        }
    }
}