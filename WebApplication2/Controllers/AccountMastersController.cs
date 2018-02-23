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
    public class AccountMastersController : Controller
    {
        private DBManager db = new DBManager();
        AccountDataLayer accountDataLayer = new AccountDataLayer();
        // GET: AccountMasters
        public ActionResult Index()
        {

            return View(accountDataLayer.ToList());
        }
        public ActionResult Month()
        {
           var data= accountDataLayer.GetProcedure("MonthView");
           return View(data);
        }
        public ActionResult Daily()
        {
            var data = accountDataLayer.GetProcedure("collectionDateView");
            return View(data);
        }

        public ActionResult DailyReceipt(String  AccountName, DateTime? startDate, DateTime? endDate)
        {
            var data = accountDataLayer.GetData("select *  from  vwCollection");
            if (AccountName==null)
            {
                AccountName = string.Empty;
            }
            ViewBag.AccountMaster = accountDataLayer.GetData("select *  from  vwAccountMaster   where AccountName like '%" + AccountName + "%' ");
            if(startDate ==null)
            {
                ViewBag.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }
            if (startDate == null)
            {
                ViewBag.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month+1, 1).AddDays(-1);
            }
            
            
            return View(data);
       }

        // GET: AccountMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountMaster accountMaster = accountDataLayer.Find(id);
            if (accountMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountMaster);
        }

        // GET: AccountMasters/Create
        public ActionResult Create()
        {
            AccountMaster accountMaster = new AccountMaster();
            accountMaster.IsActive = true;
            return View(accountMaster);
        }

        // POST: AccountMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountMaster accountMaster)
        {
            if (ModelState.IsValid)
            {
                if (accountMaster.AccountImage != null)
                {
                    accountMaster.AccountImagePath = "~/image/" + accountMaster.AccountImage.FileName;
                    accountMaster.AccountImage.SaveAs(Server.MapPath("~/image") + "/" + accountMaster.AccountImage.FileName);
                }

                
                accountDataLayer.InsertUpdate(accountMaster);
                accountDataLayer.GetData("Update AccountMaster set EndDate=NULL where  isnull(isActive,0)=1 and AccountMasterId=" + accountMaster.AccountMasterId);
                accountDataLayer.GetProcedure("GenrateCollection @startdate='" + accountMaster.StartDate + "',@EndDate='" +DateTime.Today.ToString("dd-MMM-yyyy")  +"', @AccountMasterId=" + accountMaster.AccountMasterId);
                return RedirectToAction("Index");
            }

            return View(accountMaster);
        }

        // GET: AccountMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountMaster accountMaster = accountDataLayer.Find(id);
            if (accountMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountMaster);
        }

        // POST: AccountMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AccountMaster accountMaster)
        {
            if (ModelState.IsValid)
            {
                if (accountMaster.AccountImage != null)
                {
                    accountMaster.AccountImagePath = "~/image/" + accountMaster.AccountImage.FileName;
                    accountMaster.AccountImage.SaveAs(Server.MapPath("~/image") + "/" + accountMaster.AccountImage.FileName);
                }
                accountDataLayer.InsertUpdate(accountMaster);
                accountDataLayer.GetData("Update AccountMaster set EndDate=NULL where isnull(isActive,0)=1 and AccountMasterId" + accountMaster.AccountMasterId);
                return RedirectToAction("Index");
            }
            return View(accountMaster);
        }

        // GET: AccountMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountMaster accountMaster = accountDataLayer.Find(id);
            if (accountMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountMaster);
        }

        // POST: AccountMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            accountDataLayer.Deleted(id);
            return RedirectToAction("Index");
        }

        public ActionResult CollectionDay(DateTime StartDate)
        {
         //   StartDate = StartDate.AddDays(1);

            var data = accountDataLayer.GetProcedure("GenrateCollection @startDate='" + StartDate.ToString("dd-MMM-yyy") + "'");
            return RedirectToAction("DailyReceipt");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
