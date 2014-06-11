using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QueuesApp.Controllers
{
    public class SQLServiceProviderController : Controller
    {
        // GET: SQLServiceProvider
        public ActionResult Index()
        {
            return View();
        }

        // GET: SQLServiceProvider/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SQLServiceProvider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SQLServiceProvider/Create
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

        // GET: SQLServiceProvider/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SQLServiceProvider/Edit/5
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

        // GET: SQLServiceProvider/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SQLServiceProvider/Delete/5
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
