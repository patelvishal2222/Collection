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
    public class CollectionsController : Controller
    {
        //private DBManager db = new DBManager();
        CollectionDataLayer CollectionDataLayer = new CollectionDataLayer();
        AccountDataLayer AccountDataLayer = new AccountDataLayer();

        // GET: Collections
        public ActionResult Index()
        {
            return View(CollectionDataLayer.ToList());
        }
        public ActionResult AccountDisplay(int ? AccountMasterId)
        {
            if (AccountMasterId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Collection> collection = CollectionDataLayer.ToList().Where(x => x.AccountMasterId == AccountMasterId).ToList();
            AccountMaster AccountMaster = AccountDataLayer.Find(AccountMasterId);
            ViewBag.MonthView = AccountDataLayer.GetProcedure("MonthView @AccountMasteId=" + AccountMasterId.ToString());
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(AccountMaster);
        }

       

        // GET: Collections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = CollectionDataLayer.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        public ActionResult CreateEdit(int? AccountMasterId,int ? CollectionId)
        { Collection collection=new Collection();
            if(AccountMasterId!=null)
            {
                collection.AccountMasterId=Convert.ToInt32( AccountMasterId);
            }
            if (CollectionId != null)
            {
          collection  = CollectionDataLayer.Find(CollectionId);    
            }


            ViewBag.AccountMasterId = new SelectList(AccountDataLayer.ToList(), "AccountMasterId", "AccountName", collection.AccountMasterId);
            return View(collection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEdit(Collection collection)
        {
            if (ModelState.IsValid)
            {

                CollectionDataLayer.InsertUpdate(collection);
                return RedirectToAction("AccountDisplay", new { @AccountMasterId = collection.AccountMasterId });
            }

            return View(collection);
        }

        // GET: Collections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionId,ReceiptDate,Amount")] Collection collection)
        {
            if (ModelState.IsValid)
            {
               
                CollectionDataLayer.InsertUpdate(collection);
                return RedirectToAction("Index");
            }

            return View(collection);
        }

        // GET: Collections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = CollectionDataLayer.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Collection collection)
        {
            if (ModelState.IsValid)
            {
                
                CollectionDataLayer.InsertUpdate(collection);
                return RedirectToAction("AccountDisplay", new { @AccountMasterId = collection.AccountMasterId });
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = CollectionDataLayer.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        [HttpPost, ActionName("ChangeReceiptDate")]
        public void ChangeReceiptDate(int? id, string receiptDate)
        {
            CollectionDataLayer.changeReceiptDate(id, Convert.ToDateTime(receiptDate));
        }


        [HttpPost, ActionName("TransferReceiptDate")]
        public void TransferReceiptDate(string receiptDate, string transferDate)
        {
            CollectionDataLayer.TransferReceiptDate(Convert.ToDateTime(receiptDate), Convert.ToDateTime(transferDate));
        }
        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]

        public void DeleteConfirmed(int? id, int value=1)
        {

            CollectionDataLayer.Deleted(id, value);
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Test()
        {

            return View();
        }
    }

    
}
