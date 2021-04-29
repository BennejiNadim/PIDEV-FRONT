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

        string Baseurl = "http://localhost:8081/";
        
        public async Task<ActionResult> Index()
        {
            List<Furniture> furnitures = new List<Furniture>();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage Res = await client.GetAsync("api/getallFurniture");


                if (Res.IsSuccessStatusCode)
                {

                    var result = Res.Content.ReadAsAsync<IEnumerable<Furniture>>().Result;

                    furnitures = result.ToList();
                }

                return View(furnitures);
            }
        }


    }

    }


       

