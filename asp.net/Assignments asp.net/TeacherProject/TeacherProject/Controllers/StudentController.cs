using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeacherProject.Models;

namespace TeacherProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/List
        public ActionResult List(string SearchKey = null)
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);
            return View(Students);
        }

        // GET: Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        //GET: /Student/DeleteConfirm{id}
        public ActionResult DeleteConfirm(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            return View(NewStudent);
        }

        //POST: /Student/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            StudentDataController controller = new StudentDataController();
            controller.DeleteStudent(id);
            return RedirectToAction("List");
        }

        //GET: /Student/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Student/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }

        //Post: /Student/Create
        [HttpPost]
        public ActionResult Create(string StudentFname, string StudentLname, string StudentNumber, DateTime EnrolDate)
        {
            //identify that this method is running 
            //identify the inputs provied from the form

            Student NewStudent = new Student();
            NewStudent.StudentFName = StudentFname;
            NewStudent.StudentLName = StudentLname;
            NewStudent.StudentNumber = StudentNumber;
            NewStudent.EnrolDate = EnrolDate;

            StudentDataController controller = new StudentDataController();
            controller.AddStudent(NewStudent);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Student Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Student</param>
        /// <returns>A dynamic "Update Student" webpage which provides the current information of the Student and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Student/Update/5</example>
        public ActionResult Update(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }

        public ActionResult Ajax_Update(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student SelectedStudent = controller.FindStudent(id);

            return View(SelectedStudent);
        }

        /// <summary>
        /// Receives a POST request containing information about an existing Student in the system, with new values. Conveys this information to the API, and redirects to the "Student Show" page of our updated Student.
        /// </summary>
        /// <param name="id">Id of the Student to update</param>
        /// <param name="StudentFname">The updated first name of the Student</param>
        /// <param name="StudentLname">The updated last name of the Student</param>
        /// <param name="StudentNumber">The updated number of the Student.</param>
        /// <param name="EnrolDate">The updated enrol date of the Student.</param>
        /// <returns>A dynamic webpage which provides the current information of the Student.</returns>
        /// <example>
        /// POST : /Student/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"StudentFname":"Christine",
        ///	"StudentLname":"Bittle",
        ///	"StudentNumber":"N1001",
        ///	"EnrolDate":"2020-01-01 00:00:00"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string StudentFname, string StudentLname, string StudentNumber, DateTime EnrolDate)
        {
            Student StudentInfo = new Student();
            StudentInfo.StudentFName = StudentFname;
            StudentInfo.StudentLName = StudentLname;
            StudentInfo.StudentNumber = StudentNumber;
            StudentInfo.EnrolDate = EnrolDate;

            StudentDataController controller = new StudentDataController();
            controller.UpdateStudent(id, StudentInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}