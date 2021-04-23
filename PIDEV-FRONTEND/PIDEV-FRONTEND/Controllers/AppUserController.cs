using PIDEV_FRONTEND.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
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

            return View("home");
        }
        public ActionResult addPropertyView()
        {
            return View();
        }
        public ActionResult home()
        {
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
    }
}
   
