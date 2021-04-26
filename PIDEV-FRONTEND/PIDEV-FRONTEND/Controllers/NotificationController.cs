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
    public class NotificationController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:8081/";
        static readonly Notification notif1 = new Notification { notificationId = 1, notifType = "PriceIncrease" };
        static readonly Notification notif2 = new Notification { notificationId = 2, notifType = "PriceDecreace" };
        static readonly Notification notif3 = new Notification { notificationId = 3, notifType = "FavoriteSold" };
        static readonly Notification notif4 = new Notification { notificationId = 4, notifType = "FavoriteRented" };
        public async Task<ActionResult> Index()
        {
            List<Notification> notifications = new List<Notification>();

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
                HttpResponseMessage Res = await client.GetAsync("notification/getall?user_id=2");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {

                    var result = Res.Content.ReadAsAsync<IEnumerable<Notification>>().Result;
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
                    notifications = result.ToList();

                }
                //returning the employee list to view
                return View(notifications);
            }
        }

        // GET: Categories/Create
        /*
        public ActionResult Create()
        {
            ViewBag.notifType = new SelectList(new[] { notif1, notif2, notif3, notif4 }, "notifType", "notifType");
            //ViewBag.ManageurId = new SelectList(new[] { manageur1, manageur2 }, "criteriaId", "NomPrenom");
            return View();
        }

        // POST: Categories/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Notification epm)
        {
            string Baseurl = "http://localhost:8081/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // client.DefaultRequestHeaders.Add("X-Miva-API-Authorization", "MIVA xxxxxxxxxxxxxxxxxxxxxx");

                var response = await client.PostAsJsonAsync("notification/add?announcement_id=1&user_id=2", epm);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(epm);
        }

        public ActionResult Edit(int id)
        {
            Notification epm = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081/criteria/");
                //HTTP GET
                var responseTask = client.GetAsync("get?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Notification>();
                    readTask.Wait();

                    epm = readTask.Result;
                }
            }
            ViewBag.notifType = new SelectList(new[] { notif1, notif2, notif3, notif4 }, "notifType", "notifType");
            return View(epm);
        }


        //
        // POST: /Products/Edit/5

        [HttpPost]
        public async Task<ActionResult> Edit(Notification epm)
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
        }*/
    }
}