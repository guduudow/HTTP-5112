using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
    }
}
