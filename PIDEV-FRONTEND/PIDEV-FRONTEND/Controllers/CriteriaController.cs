using Newtonsoft.Json;
using PIDEV_FRONTEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PIDEV_FRONTEND.Controllers
{
    public class CriteriaController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:8081/";
        static readonly Criteria crit1 = new Criteria { criteriaId = 1, announcementType = "Sale" };
        static readonly Criteria crit2 = new Criteria { criteriaId = 2, announcementType = "Rent" };
        static readonly Criteria crit3 = new Criteria { criteriaId = 3, announcementType = "HolidayRent" };
        public async Task<ActionResult> Index()
        {
            List<Criteria> criterias = new List<Criteria>();

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                ///////
                //var _AccessToken = Session[" AccessToken "] ;
                ///////
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("criteria/getall");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {

                    var result = Res.Content.ReadAsAsync<IEnumerable<Criteria>>().Result;
                    //Storing the response details recieved from web api 
                    //var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    ////string apiResponse = await response.Content.ReadAsStringAsync();

                    //var deserializedjsonResult = Newtonsoft.Json.Linq.JObject.Parse(EmpResponse);
                    //var token = Newtonsoft.Json.Linq.JObject.Parse(EmpResponse).SelectToken("JSON");

                    //if (token.Count<object>() != 0)
                    //{
                    //    EmpInfo = token.ToObject<List<Criteria>>();
                    //}

                    //Deserializing the response recieved from web api and storing into the Employee list
                    criterias = result.ToList();

                }
                //returning the employee list to view
                return View(criterias);
            }
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.announcementType = new SelectList(new[] { crit1, crit2, crit3 }, "announcementType", "announcementType");
            //ViewBag.ManageurId = new SelectList(new[] { manageur1, manageur2 }, "criteriaId", "NomPrenom");
            return View();
        }

        // POST: Categories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Criteria epm)
        {
            string Baseurl = "http://localhost:8081/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");

                var response = await client.PostAsJsonAsync("criteria/add?user_id=2", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }

        public ActionResult Edit(int id)
        {
            Criteria epm = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/criteria/");
                //HTTP GET
                var responseTask = client.GetAsync("get?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Criteria>();
                    readTask.Wait();

                    epm = readTask.Result;
                }
            }
            ViewBag.announcementType = new SelectList(new[] { crit1, crit2, crit3 }, "announcementType", "announcementType");
            return View(epm);
        }


        //
        // POST: /Products/Edit/5

        [HttpPost]
        public async Task<ActionResult> Edit(Criteria epm)
        {
            string Baseurl = "http://localhost:8081/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");

                var response = await client.PostAsJsonAsync("criteria/add?user_id=2", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }
    }
}