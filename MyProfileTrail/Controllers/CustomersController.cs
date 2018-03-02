using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyProfileTrail.Models;
using Resources;
using MyProfileTrail.FutureSparkUtility;
using System.IO;

namespace MyProfileTrail.Controllers
{
    public class CustomersController : Controller
    {
        private FunManageDBEntities db = new FunManageDBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //GET: Customers/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Customers/Login
        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")] LoginModel loginForm)
        {
            if (ModelState.IsValid)
            {
                string loginCusName = string.Empty;
                bool isCusExist = CustomerDAL.IsCustomer(db.Customers, loginForm, out loginCusName);
                IEnumerable<Customer> customer = db.Customers.Where(c => c.Email.Equals(loginForm.Email) && c.Password.Equals(loginForm.Password));

                if (isCusExist)
                {
                    Session[Constant.SK_LOGIN] = Constant.INDICATOR_Y;
                    return Json(Url.Action("Index", "Home"));
                }
                else
                {
                    Session[Constant.SK_LOGIN] = Constant.INDICATOR_N;
                    return Json("Fail");
                }
            }

            return View();            
        }

        // GET: Customers/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(HttpPostedFileBase postFile, [Bind(Include = "Id,CFirstName,CLastName,Email,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Role = Constant.ROLE_EMPLOYER;
                customer.Password = AesEncryptamajig.Encrypt(customer.Password, AesEncryptamajig.getKey());
                db.Customers.Add(customer);
                db.SaveChanges();

                //upload profile photo
                if(postFile != null)
                {
                    string path = Server.MapPath("~/ProfilePic/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    postFile.SaveAs(path + customer.Id);
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CFirstName,CLastName,Email,Password,Role")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
