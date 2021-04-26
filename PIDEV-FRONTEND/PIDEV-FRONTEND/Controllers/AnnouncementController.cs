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
    public class AnnouncementController : Controller
    {
        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:8081/";
        static readonly Announcement ann1 = new Announcement { announcementId = 1, type = "Sale" };
        static readonly Announcement ann2 = new Announcement { announcementId = 2, type = "Rent" };
        static readonly Announcement ann3 = new Announcement { announcementId = 3, type = "HolidayRent" };
        public async Task<ActionResult> Index()
        {
            List<Announcement> announcements = new List<Announcement>();
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
                HttpResponseMessage Res = await client.GetAsync("search/");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {

                    var result = Res.Content.ReadAsAsync<IEnumerable<Announcement>>().Result;
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
                    announcements = result.ToList();
                }
                //returning the employee list to view
                return View(announcements);
            }
        }


    }
}