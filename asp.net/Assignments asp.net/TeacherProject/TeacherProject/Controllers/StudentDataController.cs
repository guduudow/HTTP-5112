using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeacherProject.Models;
using System.Data.SqlClient;
using System.Web.Http.Cors;

namespace TeacherProject.Controllers
{
    public class StudentDataController : ApiController
    {
        //database class which allows me to connect to my database
        private SchoolDbContext School = new SchoolDbContext();

        //this controller will allow access to a list of students in the school
        ///<summary>
        ///Returns a list of students in the system
        /// </summary>
        /// <example>
        /// GET/api/StudentData/ListStudents
        /// </example>
        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey?}")]
        public IEnumerable<Student> ListStudents(string SearchKey = null)
        {
            //create connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new query for the database
            MySqlCommand command = conn.CreateCommand();
            //SQL Query goes here
            command.CommandText = "Select * from Students where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname, ' ', studentlname)) like lower(@key)";
            command.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            command.Prepare();
            //Gather result set of query into a variable
            MySqlDataReader reader = command.ExecuteReader();
            //create an empty list of students
            List<Student> StudentData = new List<Student>();
            //loop through each row of the result set
            while (reader.Read())
            {
                //access column info by database column name as index
                int studentId = Convert.ToInt32(reader["studentid"]);
                string studentFname = reader["studentfname"].ToString();
                string studentLname = reader["studentlname"].ToString();
                string studentnumber = reader["studentnumber"].ToString();
                DateTime enroldate = (DateTime)reader["enroldate"];
                //string StudentInfo = reader["studentid"] + " " + reader["studentfname"] + " " + reader["studentlname"] + " " + reader["studentnumber"] + " " + reader["enroldate"];

                Student NewStudent = new Student();
                NewStudent.StudentID = studentId;
                NewStudent.StudentFName = studentFname;
                NewStudent.StudentLName = studentLname;
                NewStudent.StudentNumber = studentnumber;
                NewStudent.EnrolDate = enroldate;
                //add data to the list
                StudentData.Add(NewStudent);
            }
            //close the connection
            conn.Close();
            //return the final list of teachers
            return StudentData;
        }

        ///<summary>
        ///Find a Student in the system given an id
        /// </summary>
        /// <param name="id">The student primary key</param>
        /// <returns>A student object</returns>
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //create instance of new connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection 
            conn.Open();

            //establish a new query for database
            MySqlCommand cmd = conn.CreateCommand();

            //write out query
            cmd.CommandText = "SELECT * FROM STUDENTS WHERE studentid =" + id;

            //gather results into a variable
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //access column information by the database column name as an index
                int studentId = Convert.ToInt32(reader["studentid"]);
                string studentFname = reader["studentfname"].ToString();
                string studentLname = reader["studentlname"].ToString();
                string studentnumber = reader["studentnumber"].ToString();
                DateTime enroldate = (DateTime)reader["enroldate"];


                NewStudent.StudentID = studentId;
                NewStudent.StudentFName = studentFname;
                NewStudent.StudentLName = studentLname;
                NewStudent.StudentNumber = studentnumber;
                NewStudent.EnrolDate = enroldate;
            }

            return NewStudent;
        }

        ///<summary>
        ///Deletes a student from the connected mySQL Database if the ID of that student exists
        /// </summary>
        /// <param name="=id">The ID of the Student</param>
        /// <example>
        /// POST /api/StudentData/DeleteStudent/3
        /// </example>
        /// 
        [HttpPost]
        public void DeleteStudent(int id)
        {
            //create an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection between the web server and database
            conn.Open();

            //establish a new command (query) for the database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from students where studentid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection
            conn.Close();
        }

        ///<summary>
        ///adds a student to the mySQL database
        /// </summary>
        /// <param name="NewStudent">An object with fields that map to the columns of the student's table</param>
        /// <example>
        /// Post api/StudentData/AddStudent
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "StudentFname": Ederes
        ///     "StudentLname": Gure
        ///     "StudentNumber": N1001
        ///     "EnrolDate": 2023-12-05 00:00:00
        /// }
        /// </example>
        /// 
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddStudent([FromBody] Student NewStudent)
        {
            //create an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //open the conecction between the web server and database
            conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into students (studentfname, studentlname, studentnumber, enroldate) values (@StudentFname,@StudentLname,@StudentNumber,@EnrolDate)";
            cmd.Parameters.AddWithValue("@StudentFname", NewStudent.StudentFName);
            cmd.Parameters.AddWithValue("@StudentLname", NewStudent.StudentLName);
            cmd.Parameters.AddWithValue("@StudentNumber", NewStudent.StudentNumber);
            cmd.Parameters.AddWithValue("@EnrolDate", NewStudent.EnrolDate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection
            conn.Close();
        }

        ///<summary>
        ///Updates a student on the MySQL Database. Non-deterministic.
        /// </summary>
        /// <param name="StudentInfo">An object with fields that map to column of the student table</param>
        /// <example>
        /// POST api/StudentData/UpdateStudent/209
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "StudentFname": Ederes
        ///     "StudentLname": Gure
        ///     "StudentNumber": N3004
        ///     "EnrolDate": 2024-01-20 00:00:00
        /// }
        /// </example>
        /// 
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateStudent(int id, [FromBody] Student StudentInfo)
        {
            //create a conenction
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection
            Conn.Open();

            //estbalish a new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update students set studentfname=@StudentFname, studentlname=@StudentLname, studentnumber=@StudentNumber, enroldate=@EnrolDate where studentid=@StudentID";
            cmd.Parameters.AddWithValue("@StudentFname", StudentInfo.StudentFName);
            cmd.Parameters.AddWithValue("@StudentLname", StudentInfo.StudentLName);
            cmd.Parameters.AddWithValue("@StudentNumber", StudentInfo.StudentNumber);
            cmd.Parameters.AddWithValue("@EnrolDate", StudentInfo.EnrolDate);
            cmd.Parameters.AddWithValue("@StudentID", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}

