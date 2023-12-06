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

        //GET: /Course/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            CourseDataController controller = new CourseDataController();
            Course NewCourse = controller.FindCourse(id);

            return View(NewCourse);
        }

        //POST: /Course/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            CourseDataController controller = new CourseDataController();
            controller.DeleteCourse(id);
            return RedirectToAction("List");
        }

        //GET: /Course/New
        public ActionResult New() 
        { 
            return View(); 
        }

        //GET: /Course/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }

        //POST: /Course/Create
        [HttpPost]
        public ActionResult Create(string ClassCode, int TeacherId, DateTime StartDate, DateTime FinishDate, string ClassName)
        {
            //identify that this method is running
            //identify the inputs provided from the form

            Course NewCourse = new Course();
            NewCourse.ClassCode = ClassCode;
            NewCourse.TeacherID = TeacherId;
            NewCourse.StartDate = StartDate;
            NewCourse.FinishDate = FinishDate;
            NewCourse.ClassName = ClassName;

            CourseDataController controller = new CourseDataController();
            controller.AddCourse(NewCourse);

            return RedirectToAction("List");

        }

        /// <summary>
        /// Routes to a dynamically generated "Course Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Course</param>
        /// <returns>A dynamic "Update Course" webpage which provides the current information of the Course and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Course/Update/5</example>
        public ActionResult Update(int id)
        {
            CourseDataController controller = new CourseDataController();
            Course SelectedCourse = controller.FindCourse(id);

            return View(SelectedCourse);
        }

        public ActionResult Ajax_Update(int id)
        {
            CourseDataController controller = new CourseDataController();
            Course SelectedCourse = controller.FindCourse(id);

            return View(SelectedCourse);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing Course in the system, with new values. Conveys this information to the API, and redirects to the "Course Show" page of our updated Course.
        /// </summary>
        /// <param name="id">Id of the Course to update</param>
        /// <param name="ClassCode">The updated code of the Course</param>
        /// <param name="TeacherID">The updated teacher of the Course</param>
        /// <param name="StartDate">The updated start date of the Course.</param>
        /// <param name="FinishDate">The updated finish date of the Course.</param>
        /// <param name=ClassName">The updated class name of the course</param>
        /// <returns>A dynamic webpage which provides the current information of the Course.</returns>
        /// <example>
        /// POST : /Course/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"ClassCode":"HTTP 5111",
        ///	"TeacherID":"3",
        ///	"StartDate":"2023-09-05",
        ///	"FinishDate":"2023-12-05"
        ///	"ClassName": "Web Development 1"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string ClassCode, int TeacherID, DateTime StartDate, DateTime FinishDate, string ClassName)
        {
            Course CourseInfo = new Course();
            CourseInfo.ClassCode = ClassCode;
            CourseInfo.TeacherID = TeacherID;
            CourseInfo.StartDate = StartDate;
            CourseInfo.FinishDate = FinishDate;
            CourseInfo.ClassName = ClassName;

            CourseDataController controller = new CourseDataController();
            controller.UpdateCourse(id, CourseInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}