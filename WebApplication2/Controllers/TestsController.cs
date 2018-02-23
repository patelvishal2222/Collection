using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TestsController : Controller
    {
       // private ApplicationDbContext db = new ApplicationDbContext();
        Test_BLayer TestDb = new Test_BLayer();
        // GET: Tests
        public ActionResult Index()
        {


            //return View(db.Tests.ToList());

            return View(TestDb.ToList());
        }

        // GET: Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          //  Test test = db.Tests.Find(id);
            Test test = TestDb.Get(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: Tests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Test test)
        {
            if (ModelState.IsValid)
            {
                test.user_image = "~/image/" + test.user_image_data.FileName;

                //store image in folder
                test.user_image_data.SaveAs(Server.MapPath("image") + "/" + test.user_image_data.FileName);

                //db.Tests.Add(test);
                //db.SaveChanges();
                TestDb.insertUpdate(test);
                return RedirectToAction("Index");
            }

            return View(test);
        }

        // GET: Tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = TestDb.Get(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Test test)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(test).State = EntityState.Modified;
                //db.SaveChanges();
                TestDb.insertUpdate(test);
                return RedirectToAction("Index");
            }
            return View(test);
        }

        // GET: Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = TestDb.Get(id); ;
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             TestDb.Delete(id);
            //db.Tests.Remove(test);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                
            }
            base.Dispose(disposing);
        }
     public    ActionResult Display()
        {
            return View();

        }
    }

    

}
