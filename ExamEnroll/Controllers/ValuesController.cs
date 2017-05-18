using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamEnroll;
using ExamEnroll.App_Start.FilterAuthorize;

namespace ExamEnroll.Controllers
{
    public class ValuesController : Controller
    {
        private ExamEnrollDBEntities db = new ExamEnrollDBEntities();

        [CheckAdminLogin]
        // GET: Values
        public async Task<ActionResult> Index()
        {
            return View(await db.Value.ToListAsync());
        }

        [CheckAdminLogin]
        // GET: Values/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = await db.Value.FindAsync(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        [CheckAdminLogin]
        // GET: Values/Create
        public ActionResult Create()
        {
            return View();
        }

        [CheckAdminLogin]
        // POST: Values/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Value1")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Value.Add(value);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(value);
        }

        [CheckAdminLogin]
        // GET: Values/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = await db.Value.FindAsync(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        [CheckAdminLogin]
        // POST: Values/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Value1")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Entry(value).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(value);
        }

        [CheckAdminLogin]
        // GET: Values/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = await db.Value.FindAsync(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // POST: Values/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Value value = await db.Value.FindAsync(id);
            db.Value.Remove(value);
            await db.SaveChangesAsync();
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
