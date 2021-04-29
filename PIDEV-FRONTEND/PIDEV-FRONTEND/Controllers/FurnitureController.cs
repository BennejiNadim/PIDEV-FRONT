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



        // GET: AppUser
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/getallFurniture").Result;
               
             List<Furniture> furn = new List<Furniture>();

          
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<Furniture>>().Result;
            }
            else
            {
                //                ViewBag.result = "error";
                list = null;
            }
           
            return View(furn);
        }

        // GET: AppUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUser/Create
        public ActionResult Create()
        {
            return View();
        }












    }

}
       

