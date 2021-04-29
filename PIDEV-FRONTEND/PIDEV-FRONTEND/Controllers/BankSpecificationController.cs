using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PIDEV_FRONTEND.Models;

namespace PIDEV_FRONTEND.Controllers
{
    public class BankSpecificationController : Controller
    {
        // GET: BankSpecification
        public ActionResult Index()
        {
            IEnumerable<BankSpecification> result = WebApiHelper.Get<IEnumerable<BankSpecification>>("bank/");

            return View(result);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(BankSpecification bs)
        {
            WebApiHelper.Post<BankSpecification>("bank/", bs);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {

            BankSpecification bs = WebApiHelper.Get<BankSpecification>("bank/" + id.ToString());

            return View(bs);

        }
        [HttpPost]
        public ActionResult Edit(BankSpecification bs)
        {
            WebApiHelper.Put<BankSpecification>("bank/", bs);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            WebApiHelper.Delete<object>("bank/" + id.ToString());
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Details(long id)
        {

            BankSpecification bs = WebApiHelper.Get<BankSpecification>("bank/" + id.ToString());

            return View(bs);

        }
    }
}
