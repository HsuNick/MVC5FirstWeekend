using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5FirstWeekend.Models;
using System.Data.Entity.Validation;

namespace MVC5FirstWeekend.Controllers
{
    public class 客戶聯絡人管理Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public bool 驗證客戶資料裡的客戶聯絡人Email是否重複(string 客戶聯絡人Email, int 客戶Id)
        {
            var 驗證信箱是否重複 = db.Database.SqlQuery<客戶聯絡人>("SELECT * FROM dbo.客戶聯絡人 WHERE Email=@p0 AND 客戶Id=@p1", 客戶聯絡人Email, 客戶Id);

            if (驗證信箱是否重複.Count() > 0)
                return true;
            else
                return false;
        }

        // GET: 客戶聯絡人管理
        public ActionResult Index()
        {
            //var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);

            var all = db.客戶聯絡人.AsQueryable();

            var data = all.Where(p => p.是否已刪除 == false)
                .OrderByDescending(p => p.客戶Id);

            return View(data);
        }

        // GET: 客戶聯絡人管理/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人管理/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人管理/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            bool email是否重複 = 驗證客戶資料裡的客戶聯絡人Email是否重複(客戶聯絡人.Email, 客戶聯絡人.客戶Id);

            if (email是否重複 == false)
            { 
                if (ModelState.IsValid)
                { 
                    db.客戶聯絡人.Add(客戶聯絡人);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人管理/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人管理/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            bool email是否重複 = 驗證客戶資料裡的客戶聯絡人Email是否重複(客戶聯絡人.Email, 客戶聯絡人.客戶Id);

            if (email是否重複 == false)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(客戶聯絡人).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人管理/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人管理/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.是否已刪除 = true;

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
