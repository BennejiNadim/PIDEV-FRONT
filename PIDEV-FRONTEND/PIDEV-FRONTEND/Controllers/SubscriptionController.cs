using PIDEV_FRONTEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PIDEV_FRONTEND.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Subscription
        public ActionResult Index()
        {
            return View();
        }

        // GET: Subscription/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult SubscriptionPricing(int amount, string subType)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            Subscription sub = new Subscription();
            sub.amount = amount;
            sub.subType = subType;
            client.PostAsJsonAsync<Subscription>("/ActivateSubscription", sub).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            ViewBag.signup = "Sign up successful";
            return View();
        }

        public ActionResult BasicSubscription()
        {

            return View();
        }
        public ActionResult GoldSubscription()
        {

            return View();
        }
        public ActionResult PremiumSubscription()
        {

            return View();
        }

        public ActionResult SubscriptionInfo()
        {

            return View();
        }
        
        public ActionResult ConfirmPayment()
        {
            return View();
        }
        // GET: Subscription/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscription/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subscription/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Subscription/Edit/5
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

        // GET: Subscription/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Subscription/Delete/5
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
    }
}
