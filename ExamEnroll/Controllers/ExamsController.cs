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
    public class ExamsController : Controller
    {
        private ExamEnrollDBEntities db = new ExamEnrollDBEntities();
        [CheckAdminLogin]
        // GET: Exams
        public async Task<ActionResult> Index()
        {
            var exam = db.Exam.Include(e => e.Course).Include(e => e.ExamTime).Include(e => e.Teacher).Include(e => e.User).Include(e => e.Value1);
            return View(await exam.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exams/Create
        public ActionResult Create()
        {
            ViewBag.CourseName = new SelectList(db.Course, "CourseName", "CourseName");
            ViewBag.Time = new SelectList(db.ExamTime, "Time", "Time");
            ViewBag.Tid = new SelectList(db.Teacher, "Tid", "Name");
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password");
            ViewBag.Value = new SelectList(db.Value, "Value1", "Value1");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Eid,UserName,CourseName,Tid,Time,Value")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exam.Add(exam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseName = new SelectList(db.Course, "CourseName", "Details", exam.CourseName);
            ViewBag.Time = new SelectList(db.ExamTime, "Time", "Time", exam.Time);
            ViewBag.Tid = new SelectList(db.Teacher, "Tid", "Name", exam.Tid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", exam.UserName);
            ViewBag.Value = new SelectList(db.Value, "Value1", "Value1", exam.Value);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseName = new SelectList(db.Course, "CourseName", "Details", exam.CourseName);
            ViewBag.Time = new SelectList(db.ExamTime, "Time", "Time", exam.Time);
            ViewBag.Tid = new SelectList(db.Teacher, "Tid", "Name", exam.Tid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", exam.UserName);
            ViewBag.Value = new SelectList(db.Value, "Value1", "Value1", exam.Value);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Eid,UserName,CourseName,Tid,Time,Value")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseName = new SelectList(db.Course, "CourseName", "Details", exam.CourseName);
            ViewBag.Time = new SelectList(db.ExamTime, "Time", "Time", exam.Time);
            ViewBag.Tid = new SelectList(db.Teacher, "Tid", "Name", exam.Tid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", exam.UserName);
            ViewBag.Value = new SelectList(db.Value, "Value1", "Value1", exam.Value);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exam.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exam exam = await db.Exam.FindAsync(id);
            db.Exam.Remove(exam);
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
