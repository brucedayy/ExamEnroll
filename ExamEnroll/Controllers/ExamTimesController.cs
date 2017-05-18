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
    public class ExamTimesController : Controller
    {
        private ExamEnrollDBEntities db = new ExamEnrollDBEntities();

        [CheckAdminLogin]
        // GET: ExamTimes
        public async Task<ActionResult> Index()
        {
            return View(await db.ExamTime.ToListAsync());
        }

        // GET: ExamTimes/Details/5
        public async Task<ActionResult> Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTime examTime = await db.ExamTime.FindAsync(id);
            if (examTime == null)
            {
                return HttpNotFound();
            }
            return View(examTime);
        }

        // GET: ExamTimes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Time")] ExamTime examTime)
        {
            if (ModelState.IsValid)
            {
                db.ExamTime.Add(examTime);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(examTime);
        }

        // GET: ExamTimes/Edit/5
        public async Task<ActionResult> Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTime examTime = await db.ExamTime.FindAsync(id);
            if (examTime == null)
            {
                return HttpNotFound();
            }
            return View(examTime);
        }

        // POST: ExamTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Time")] ExamTime examTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examTime).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(examTime);
        }

        // GET: ExamTimes/Delete/5
        public async Task<ActionResult> Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamTime examTime = await db.ExamTime.FindAsync(id);
            if (examTime == null)
            {
                return HttpNotFound();
            }
            return View(examTime);
        }

        // POST: ExamTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(DateTime id)
        {
            ExamTime examTime = await db.ExamTime.FindAsync(id);
            db.ExamTime.Remove(examTime);
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
