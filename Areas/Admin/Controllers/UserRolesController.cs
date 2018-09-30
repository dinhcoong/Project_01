using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvcweb.Models;

namespace mvcweb.Areas.Admin.Controllers
{
    public class UserRolesController : Controller
    {
        private dinhlvEntities db = new dinhlvEntities();
        [HttpPost]
        public ActionResult Index(MyListRole objview)
        {
            // added by dinhlv
            List<Role> lHob = new List<Role>();
            lHob = objview.ListRole;//Now you get All CheckBox With Post Selected Value
            return View();
        }
        // GET: UserRoles
        [HttpGet]
        public ActionResult Index()
        {
            // added by dinhlv
            MyListRole objview = new MyListRole();
            List<Role> lHob = db.Roles.ToList();
            objview.ListRole = lHob;//Now you get All CheckBox With Post Selected Value
            return View(db.UserRoles.ToList());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserRoleId,RoleId,UserId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userRole);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }
        //added by dinh
        // GET: UserRoles/EditByUserId/5
        public ActionResult EditByUserId(int? id)
        {
            var userObj = new Users();
            ViewModel vmodelObj = new ViewModel();
            using (dinhlvEntities db = new dinhlvEntities())
            {

                var UserRoleObj = db.UserRoles.Where(o => o.UserId == id).ToList();
                var UserObj = db.Users1.Where(o => o.UserId == id).FirstOrDefault();
                var ListRole = db.Roles.ToList();// lay tat ca cac role
                for (int i = 0; i < ListRole.Count(); i++)
                {
                    for (int j = 0; j < UserRoleObj.Count; j++)
                    {
                        if (ListRole[i].RoleId == UserRoleObj[j].RoleId)
                        {
                            ListRole[i].IsSelected = true;
                        }


                    }

                }
                List<Users> lstobj = new List<Users> { UserObj };
                vmodelObj.Users = lstobj;
                vmodelObj.Roles = ListRole;

                return View(vmodelObj);
            }
        }

        public ActionResult UpdateRole(string ListRoleid, int UserId)
        {
            var result = "Fail";
            var roleitemid = ListRoleid.Split(',');
            using (dinhlvEntities db = new dinhlvEntities())
            {

                //xoa du lieu cu
                List<UserRole> userRole = db.UserRoles.Where(u => u.UserId == UserId).ToList();
                if (userRole != null)
                {
                    foreach (var item in userRole)
                    {
                        db.UserRoles.Remove(item);
                        db.SaveChanges();
                    }

                }
                //cap nhat du lieu moi
                foreach (var item in roleitemid)
                {
                    if (item != string.Empty)
                    {
                        db.UserRoles.Add(new UserRole { UserId = UserId, RoleId = int.Parse(item) });
                        db.SaveChanges();
                    }
                }
                result = "Success";
                // return RedirectToAction("Index");
            }
           // return View();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserRoleId,RoleId,UserId")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userRole);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRole userRole = db.UserRoles.Find(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserRole userRole = db.UserRoles.Find(id);
            db.UserRoles.Remove(userRole);
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
