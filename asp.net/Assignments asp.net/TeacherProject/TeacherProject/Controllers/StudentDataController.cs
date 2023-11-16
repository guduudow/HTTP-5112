using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeacherProject.Models;
using System.Data.SqlClient;

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
        public IEnumerable<Student> ListStudents()
        {
            //create connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new query for the database
            MySqlCommand command = conn.CreateCommand();
            //SQL Query goes here
            command.CommandText = "SELECT * FROM STUDENTS";
            //Gather result set of query into a variable
            MySqlDataReader reader = command.ExecuteReader();
            //create an empty list of teachers
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

    }
}

