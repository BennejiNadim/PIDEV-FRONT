using PIDEV_FRONTEND.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PIDEV_FRONTEND.Controllers
{
    public class AppUserController : Controller
    {
        // GET: AppUser
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("admin/users").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<AppUser>>().Result;
            }
            else
            {
                ViewBag.result = "error";
            }
            ViewBag.signup = "";
            ViewBag.signin = "";
            return View("home");
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

        // POST: AppUser/Create
        [HttpPost]
        public ActionResult Create(string firstName, string lastName, string password, string email)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            AppUser user = new AppUser();
            user.email = email;
            user.firstName = firstName;
            user.lastName = lastName;
            user.password = password;
            client.PostAsJsonAsync<AppUser>("registerApi/Signup", user).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            ViewBag.signup = "Sign up successful";
            return View("home");
        }

        // GET: AppUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUser/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> LoginAsync(string email, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            LoginForm log = new LoginForm();
            log.email = email;
            log.password = password;
            String invalidCredentials = "";

            HttpResponseMessage response = await client.PostAsJsonAsync<LoginForm>("login", log).ContinueWith((postTask) => postTask.Result);
            if (response.IsSuccessStatusCode)
            {
                var jwtEncodedString = response.Headers.GetValues("Authorization").First();
                var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
                string jemail = token.Claims.First(c => c.Type == "sub").Value;
                string roles = token.Claims.First(c => c.Type == "roles").Value;
                HttpCookie userCookie = new HttpCookie("Token", jwtEncodedString);
                userCookie.Expires.AddDays(10);
                HttpContext.Response.SetCookie(userCookie);
                HttpCookie newCookie = Request.Cookies["Token"];
                Console.WriteLine(newCookie.Value);

            }
            else
            {
                invalidCredentials = "Invalid Credentials";
                ViewBag.err = invalidCredentials;
                return View("home");
            }
            return View("home");
        }
        public ActionResult Logout()
        {
            if (HttpContext.Request.Cookies.AllKeys.Contains("Token"))
            {
                Request.Cookies["Token"].Expires = DateTime.Now.AddYears(-1);
                HttpContext.Response.SetCookie(Request.Cookies["Token"]);
            }

            return RedirectToAction("home");
        }
        public ActionResult addPropertyView()
        {
            return View();
        }
        public ActionResult home()
        {
            ViewBag.user = this.currentUser();
            return View();
        }
        public async System.Threading.Tasks.Task<ActionResult> AddAnnouncementAsync(string estateType, string location, double price)
        {
            Announcement ann = new Announcement();
            ann.estateType = estateType;
            ann.location = location;
            ann.price = price;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            string jwtEncodedString = Request.Cookies["Token"].Value;
            Debug.WriteLine(jwtEncodedString);
            // var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            HttpResponseMessage response = await client.PostAsJsonAsync<Announcement>("apiHF/addAnnounce", ann).ContinueWith((postTask) => postTask.Result);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.status = response.StatusCode;
                ViewBag.auth = client.DefaultRequestHeaders.Authorization;
            }
            else
            {
                ViewBag.status = response.StatusCode;
                ViewBag.auth = client.DefaultRequestHeaders.Authorization;
                Debug.WriteLine(response.ToString());
                Debug.WriteLine(client.DefaultRequestHeaders.Authorization);
                Debug.WriteLine(location);
                Debug.WriteLine(estateType);
                Debug.WriteLine(price);
            }
            return View("home");
        }

        public async System.Threading.Tasks.Task<string> usernameAsync()
        {
            string jwtEncodedString = Request.Cookies["Token"].Value;
            string username = "";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            HttpResponseMessage response = client.GetAsync("userapi/loggedInUsername").Result;
            if (response.IsSuccessStatusCode)
            {
                username = await response.Content.ReadAsStringAsync();

            }
            return username;
        }
        public AppUser currentUser()
        {
            AppUser user = new AppUser();
            if (HttpContext.Request.Cookies.AllKeys.Contains("Token"))
            {
                string jwtEncodedString = Request.Cookies["Token"].Value;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8081");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
                HttpResponseMessage response = client.GetAsync("userapi/currentUser").Result;
                if (response.IsSuccessStatusCode)
                {
                    user = response.Content.ReadAsAsync<AppUser>().Result;
                }

            }
            return user;
        }
        public ActionResult profile()
        {
            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult dashboard()
        {
            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult bookmarks()
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string jwtEncodedString = Request.Cookies["Token"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            HttpResponseMessage response = client.GetAsync("apiHF/Favourites").Result;

            ViewBag.favourites = response.Content.ReadAsAsync<IEnumerable<Announcement>>().Result;

            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult myproperty()
        {
            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult submitPropertyDashboard()
        {
            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult changePassword()
        {
            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult updateProfile(string firstName, string lastName, string email, int phoneNumber, string adress, string aboutMe)
        {
            AppUser user = new AppUser();
            if (firstName != null)
            { user.firstName = firstName; }
            if (lastName != null)
            { user.lastName = lastName; }
            if (email != null)
            { user.email = email; }
            if (phoneNumber != 0)
            { user.phoneNumber = phoneNumber; }
            { user.phoneNumber = phoneNumber; }
            if (adress != null)
            { user.adress = adress; }
            if (aboutMe != null)
            { user.aboutMe = aboutMe; }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            string jwtEncodedString = Request.Cookies["Token"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            HttpResponseMessage msg = client.PostAsJsonAsync<AppUser>("userapi/updateProfile", user).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;
            ViewBag.user = this.currentUser();
            return RedirectToAction("profile");
        }
        [HttpPost]
        public ActionResult saveImage(HttpPostedFileBase image)
        {
            var img = Path.GetFileName(image.FileName);
            if (img != null && img.Length > 0)
            {
                var path = Path.Combine(Server.MapPath("~/Content/img/profileImages"), img);
                image.SaveAs(path);
                AppUser user = new AppUser();
                user.profilePic = img;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8081");
                string jwtEncodedString = Request.Cookies["Token"].Value;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
                HttpResponseMessage msg = client.PostAsJsonAsync<AppUser>("userapi/updateProfile", user).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;
            }


            ViewBag.user = this.currentUser();
            return RedirectToAction("profile");
        }
        [HttpPost]
        public ActionResult changePassword(string old, string newPass, string newPassConfirm)
        {
            if (newPass.Equals(newPassConfirm))
            {
                PassForm p = new PassForm();
                p.old = old;
                p.newPass = newPass;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8081");
                string jwtEncodedString = Request.Cookies["Token"].Value;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
                HttpResponseMessage msg = client.PostAsJsonAsync<PassForm>("userapi/updatePassword", p).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;
            }
            ViewBag.user = this.currentUser();
            return RedirectToAction("profile");
        }

        public ActionResult Listings()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("apiHF/getAnnounces").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.anns = response.Content.ReadAsAsync<IEnumerable<Announcement>>().Result;
            }
            else
            {
                ViewBag.anns = "error";
            }


            ViewBag.user = this.currentUser();
            return View();
        }
        public ActionResult singleProperty(int announcementId)
        {
            HttpClient client = new HttpClient();
            //  client.BaseAddress = new Uri("http://localhost:8081");
            string jwtEncodedString = Request.Cookies["Token"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            UriBuilder builder = new UriBuilder("http://localhost:8081/apiHF/Announce");
            builder.Query = "id=" + announcementId;
            HttpResponseMessage response = client.GetAsync(builder.Uri).Result;

            ViewBag.announcement = response.Content.ReadAsAsync<Announcement>().Result;
            Debug.WriteLine("response : " + response.Content.ReadAsAsync<Announcement>().Result);

            return View();
        }
        public ActionResult saveToBookmarks(int announcementId)
        {
            HttpClient client = new HttpClient();
            string jwtEncodedString = Request.Cookies["Token"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            UriBuilder builder = new UriBuilder("http://localhost:8081/apiHF/addToFavourite");
            builder.Query = "id=" + announcementId;
            HttpResponseMessage response = client.PostAsync(builder.Uri.ToString(), null).Result;
            return RedirectToAction("singleProperty", "AppUser", new { @announcementId = announcementId });
        }
        public ActionResult deleteFromFavourite(int announcementId)
        {
            HttpClient client = new HttpClient();
            string jwtEncodedString = Request.Cookies["Token"].Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            UriBuilder builder = new UriBuilder("http://localhost:8081/apiHF/DeleteFromFavourite");
            builder.Query = "id=" + announcementId;
            HttpResponseMessage response = client.DeleteAsync(builder.Uri.ToString()).Result;

            return RedirectToAction("bookmarks");
        }





        // adding Furniture
        public async System.Threading.Tasks.Task<ActionResult> AddFurnitureAsync(string furnitureName, string fabricator, double shippingPrice)
        {
            Furniture furn = new Furniture();
            furn.furnitureName = furnitureName;
            furn.fabricator = fabricator;
            furn.shippingPrice = shippingPrice;


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            string jwtEncodedString = Request.Cookies["Token"].Value;
            Debug.WriteLine(jwtEncodedString);
            // var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtEncodedString);
            HttpResponseMessage response = await client.PostAsJsonAsync<Furniture>("api/addFurniture", furn).ContinueWith((postTask) => postTask.Result);
            if (response.IsSuccessStatusCode)
            {
                ViewBag.status = response.StatusCode;
                ViewBag.auth = client.DefaultRequestHeaders.Authorization;
            }
            else
            {
                ViewBag.status = response.StatusCode;
                ViewBag.auth = client.DefaultRequestHeaders.Authorization;
                Debug.WriteLine(response.ToString());
                Debug.WriteLine(client.DefaultRequestHeaders.Authorization);
                Debug.WriteLine(furnitureName);
                Debug.WriteLine(fabricator);
                Debug.WriteLine(shippingPrice);
            }
            return View("home");
        }
    



    }
    }

   
