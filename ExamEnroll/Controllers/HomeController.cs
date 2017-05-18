using ExamEnroll.App_Start.FilterAuthorize;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExamEnroll.Controllers
{
    public class HomeController : Controller
    {
        private ExamEnrollDBEntities db = new ExamEnrollDBEntities();
        public async Task<ActionResult> Index()
        {
            return View(await db.New.ToListAsync());
        }      

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user)
        {
            var checkUser = from p in db.User
                            where p.UserName == user.UserName && p.Password == user.Password
                            select p;
            if (checkUser.Count() == 0)
                return View();
            else
            {
                Session["username"] = user.UserName;
                return new RedirectResult("/Home/Index");
            }            
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["username"] = null;
            return new RedirectResult("/Home/Login");
        }

        [CheckAdminLogin]
        [HttpGet]
        public ActionResult AdminIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var checkUser = from p in db.Admin
                            where p.UserName == admin.UserName && p.Password == admin.Password
                            select p;
            if (checkUser.Count() == 0)
                return View();
            else
            {
                Session["adminuser"] = admin.UserName;
                return new RedirectResult("/Home/AdminIndex");
            }
        }

        [HttpGet]
        public ActionResult AdminLogout()
        {
            Session["adminuser"] = null;
            return new RedirectResult("/Home/AdminLogin");
        }

        [CheckUserLogin]
        [HttpGet]
        public ActionResult Enroll()
        {
            ViewBag.CourseName = new SelectList(db.Course, "CourseName", "CourseName");
            ViewBag.Time = new SelectList(db.ExamTime, "Time", "Time");
            ViewBag.Tid = new SelectList(db.Teacher, "Tid", "Name");
            ViewBag.UserName = new SelectList(db.User, "UserName", "UserName");
            ViewBag.Value = new SelectList(db.Value, "Value1", "Value1");
            return View();
        }

        [CheckUserLogin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enroll([Bind(Include = "Eid,UserName,CourseName,Tid,Time,Value")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exam.Add(exam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseName = new SelectList(db.Course, "CourseName", "CourseName", exam.CourseName);
            ViewBag.Time = new SelectList(db.ExamTime, "Time", "Time", exam.Time);
            ViewBag.Tid = new SelectList(db.Teacher, "Tid", "Name", exam.Tid);
            ViewBag.UserName = new SelectList(db.User, "UserName", "Password", exam.UserName);
            ViewBag.Value = new SelectList(db.Value, "Value1", "Value1", exam.Value);
            return View(exam);
        }

        [CheckUserLogin]
        public async Task<ActionResult> EnrollSelf()
        {
            var exam = db.Exam.Include(e => e.Course).Include(e => e.ExamTime).Include(e => e.Teacher).Include(e => e.User).Include(e => e.Value1);
            return View(await exam.ToListAsync());
        }

    }
}