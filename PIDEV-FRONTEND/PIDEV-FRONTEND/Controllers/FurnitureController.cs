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






        //// adding Furniture
        //public async System.Threading.Tasks.Task<ActionResult> AddFurnitureAsync(string furnitureName, string fabricator, double shippingPrice)
        //{
        //    Furniture furn = new Furniture();
        //    furn.furnitureName = furnitureName;
        //    furn.fabricator = fabricator;
        //    furn.shippingPrice = shippingPrice;


        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:8081");
        //    string jwtEncodedString = Request.Cookies["Token"].Value;
        //  //  Debug.WriteLine(jwtEncodedString);
        //    // var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
        //    HttpResponseMessage response = await client.PostAsJsonAsync<Furniture>("api/addFurniture", furn).ContinueWith((postTask) => postTask.Result);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        ViewBag.status = response.StatusCode;
        //        ViewBag.auth = client.DefaultRequestHeaders.Authorization;
        //    }
        //    else
        //    {
        //        ViewBag.status = response.StatusCode;
        //        ViewBag.auth = client.DefaultRequestHeaders.Authorization;
        //        //Debug.WriteLine(response.ToString());
        //        //Debug.WriteLine(client.DefaultRequestHeaders.Authorization);
        //        //Debug.WriteLine(furnitureName);
        //        //Debug.WriteLine(fabricator);
        //        //Debug.WriteLine(shippingPrice);
        //    }
        //    return View("home");
        //}







    }

}


       

