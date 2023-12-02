using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherProject.Models;

namespace TeacherProject.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        // GET: Course/List
        public ActionResult List(string SearchKey = null)
        {
            CourseDataController controller = new CourseDataController();
            IEnumerable<Course> Course = controller.ListCourse(SearchKey);
            return View(Course);
        }

        // GET: Course/Show/{id}
        public ActionResult Show(int id)
        {
            CourseDataController controller = new CourseDataController();
            Course NewCourse = controller.FindCourse(id);

            return View(NewCourse);
        }
    }
}