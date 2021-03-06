﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcweb.Models;

namespace mvcweb.Controllers
{
    public class UsersController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();
        private MHMK obj = new MHMK();
        // GET: Users
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("LoginView", "Users");
            return View(db.Users1.ToList());
        }
        //Dinhlv added
        // GET: Users/DangNhap
        public ActionResult LoginView()
        {
            return View("Login");
        }
        public ActionResult Login(Users user)
        {
            var result = "Fail";

            using (dinhlvEntities db = new dinhlvEntities())
            {
                var UserDetail = db.Users1.Where(x => x.UserName == user.UserName).FirstOrDefault();
                if (UserDetail != null)
                {
                    if (UserDetail.PassWord == obj.Encrypt(UserDetail.UserName, user.PassWord))
                    {
                        Session["UserId"] = UserDetail.UserId;
                        Session["UserName"] = UserDetail.UserName;
                        result = "Success";
                    }
                    else { result = "fail"; }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout(Users user)
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            return View("Login");

        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users1.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Users users)
        {
            if (ModelState.IsValid)
            {
                users.PassWord = obj.Encrypt(users.UserName, users.PassWord);
                db.Users1.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users1.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,PassWord,Code,FirstName,LastName,Birthday,Adress,Email,Tel,Status")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.PassWord = obj.Encrypt(users.UserName, users.PassWord);
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users1.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users1.Find(id);
            db.Users1.Remove(users);
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
