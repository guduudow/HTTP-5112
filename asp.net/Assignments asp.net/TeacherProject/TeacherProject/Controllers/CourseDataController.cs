using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TeacherProject.Models;

namespace TeacherProject.Controllers
{
    public class CourseDataController : ApiController
    {
        //database class which allows me to connect to my database
        private SchoolDbContext School = new SchoolDbContext();

        //this controller will allow access to a list of courses in the school
        ///<summary>
        ///Returns a list of courses in the system
        /// </summary>
        /// <example>
        /// GET/api/CourseData/ListCourse
        /// </example>
        [HttpGet]
        [Route("api/CourseData/ListCourse/{SearchKey?}")]
        public IEnumerable<Course> ListCourse(string SearchKey = null)
        {
            //create connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new query for the database
            MySqlCommand command = conn.CreateCommand();
            //SQL Query goes here
            command.CommandText = "Select * from Classes where lower(classcode) like lower(@key) or lower(classname) like lower(@key)";
            command.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            command.Prepare();
            //Gather result set of query into a variable
            MySqlDataReader reader = command.ExecuteReader();
            //create an empty list of classes
            List<Course> CourseData = new List<Course>();
            //loop through each row of the result set
            while (reader.Read())
            {
                //access column info by database column name as index
                int classId = (int)reader["classid"];
                string classcode = reader["classcode"].ToString();
                int teacherId = Convert.ToInt32(reader["teacherid"]);
                DateTime startdate = (DateTime)reader["startdate"];
                DateTime finishdate = (DateTime)reader["finishdate"];
                string classname = reader["classname"].ToString();
                //string ClassInfo = reader["classId"] + " " + reader["classcode"] + " " + reader["teacherId"] + " " + reader["startdate"] + " " + reader["finishdate"] + " " + reader["classname"];

                Course NewCourse = new Course();
                NewCourse.ClassID = classId;
                NewCourse.ClassCode = classcode;
                NewCourse.TeacherID = teacherId;
                NewCourse.StartDate = startdate;
                NewCourse.FinishDate = finishdate;
                NewCourse.ClassName = classname;
                //add data to the list
                CourseData.Add(NewCourse);
            }
            //close the connection
            conn.Close();
            //return the final list of teachers
            return CourseData;
        }

        ///<summary>
        ///Find a course in the system given an id
        /// </summary>
        /// <param name="id">The course primary key</param>
        /// <returns>A course object</returns>
        [HttpGet]
        public Course FindCourse(int id)
        {
            Course NewCourse = new Course();

            //create instance of new connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection 
            conn.Open();

            //establish a new query for database
            MySqlCommand cmd = conn.CreateCommand();

            //write out query
            cmd.CommandText = "SELECT * FROM CLASSES WHERE classid =" + id;

            //gather results into a variable
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //access column information by the database column name as an index
                int classId = (int)reader["classid"];
                string classcode = reader["classcode"].ToString();
                int teacherId = Convert.ToInt32(reader["teacherid"]);
                DateTime startdate = (DateTime)reader["startdate"];
                DateTime finishdate = (DateTime)reader["finishdate"];
                string classname = reader["classname"].ToString();


                NewCourse.ClassID = classId;
                NewCourse.ClassCode = classcode;
                NewCourse.TeacherID = teacherId;
                NewCourse.StartDate = startdate;
                NewCourse.FinishDate = finishdate;
                NewCourse.ClassName = classname;
            }

            return NewCourse;
        }

        ///<summary>
        ///Deletes a course from the connected mySQL Database if the ID of that course exists 
        /// </summary>
        /// <param name="=id">The ID of the course</param>
        /// <example>
        /// POST /api/CourseData/DeleteCourse/3
        /// </example>
        /// 
        [HttpPost]
        public void DeleteCourse(int id)
        {
            //create an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new cmd
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from classes where classid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the conenction
            conn.Close();
        }

        ///<summary>
        ///adds a class to the SQL database
        /// </summary>
        /// <param name="NewCourse">An object with the fields that map to the columns of the course table</param>
        /// <example>
        /// POST api/CourseData/AddCourse
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "ClassCode": HTTP1001
        ///     "StartDate": 2023-12-05 00:00:00
        ///     "FinishDate": 2024-04-05 00:00:00
        ///     "ClassName": Intro to Web Development
        /// }
        /// </example>
        /// 
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddCourse([FromBody] Course NewCourse)
        {
            //create an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection between web sever and database
            conn.Open();

            //establish a new query for database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into classes (classcode, teacherid, startdate, finishdate, classname) values (@ClassCode,@TeacherId,@StartDate,@FinishDate,@ClassName)";
            cmd.Parameters.AddWithValue("@ClassCode", NewCourse.ClassCode);
            cmd.Parameters.AddWithValue("@TeacherId", NewCourse.TeacherID);
            cmd.Parameters.AddWithValue("@StartDate", NewCourse.StartDate);
            cmd.Parameters.AddWithValue("@FinishDate", NewCourse.FinishDate);
            cmd.Parameters.AddWithValue("@ClassName", NewCourse.ClassName);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection
            conn.Close();
        }

        ///<summary>
        ///Updates a course on the MySQL Database. Non-deterministic.
        /// </summary>
        /// <param name="CourseInfo">An object with fields that map to column of the course table</param>
        /// <example>
        /// POST api/CourseData/UpdateCourse/209
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "ClassCode": HTTP3100
        ///     "TeacherID": 4
        ///     "StartDate": 2023-09-05
        ///     "FinishDate": 2024-05-05 00:00:00
        ///     "ClassName": Strategies in Web Development
        /// }
        /// </example>
        /// 
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateCourse(int id, [FromBody] Course CourseInfo)
        {
            //create a conenction
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection
            Conn.Open();

            //estbalish a new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update classes set classcode=@ClassCode, teacherid=@TeacherID, startdate=@StartDate, finishdate=@FinishDate, classname=@ClassName where classid=@ClassID";
            cmd.Parameters.AddWithValue("@ClassCode", CourseInfo.ClassCode);
            cmd.Parameters.AddWithValue("@TeacherID", CourseInfo.TeacherID);
            cmd.Parameters.AddWithValue("@StartDate", CourseInfo.StartDate);
            cmd.Parameters.AddWithValue("@FinishDate", CourseInfo.FinishDate);
            cmd.Parameters.AddWithValue("@ClassName", CourseInfo.ClassName);
            cmd.Parameters.AddWithValue("@ClassID", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
