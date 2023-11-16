using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeacherProject.Models;
using MySql.Data.MySqlClient;

namespace TeacherProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        //database class which allows me to connect to my database
        private SchoolDbContext School = new SchoolDbContext();

        //this controller will allow access to a list of teachers in the school
        ///<summary>
        ///Returns a list of teachers in the system
        /// </summary>
        /// <example>
        /// GET/api/TeacherData/ListTeachers
        /// </example>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            //create connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new query for the database
            MySqlCommand command = conn.CreateCommand();
            //SQL Query goes here
            command.CommandText = "SELECT * FROM TEACHERS";
            //Gather result set of query into a variable
            MySqlDataReader reader = command.ExecuteReader();
            //create an empty list of teachers
            List<Teacher> TeacherData = new List<Teacher>();
            //loop through each row of the result set
            while (reader.Read())
            {
                //access column info by database column name as index
                int teacherId = (int)reader["teacherid"];
                string teacherFname = reader["teacherfname"].ToString();
                string teacherLname = reader["teacherlname"].ToString();
                string employeenumber = reader["employeenumber"].ToString();
                DateTime hiredate = (DateTime)reader["hiredate"];
                decimal salary = (decimal)reader["salary"];
                //string TeacherInfo = reader["teacherid"] + " " + reader["teacherfname"] + " " + reader["teacherlname"] + " " + reader["employeenumber"] + " " + reader["hiredate"] + " " + reader["salary"];
                
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherID = teacherId;
                NewTeacher.TeacherFName = teacherFname;
                NewTeacher.TeacherLName = teacherLname;
                NewTeacher.EmployeeNumber = employeenumber;
                NewTeacher.HireDate = hiredate;
                NewTeacher.Salary = salary;
                //add data to the list
                TeacherData.Add(NewTeacher);
            }
            //close the connection
            conn.Close();
            //return the final list of teachers
            return TeacherData;
        }

        ///<summary>
        ///Find a Teacher in the system given an id
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //create instance of new connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection 
            conn.Open();

            //establish a new query for database
            MySqlCommand cmd = conn.CreateCommand();

            //write out query
            cmd.CommandText = "SELECT * FROM TEACHERS WHERE teacherid =" + id;

            //gather results into a variable
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //access column information by the database column name as an index
                int teacherId = (int)reader["teacherid"];
                string teacherFname = reader["teacherfname"].ToString();
                string teacherLname = reader["teacherlname"].ToString();
                string employeenumber = reader["employeenumber"].ToString();
                DateTime hiredate = (DateTime)reader["hiredate"];
                decimal salary = (decimal)reader["salary"];


                NewTeacher.TeacherID = teacherId;
                NewTeacher.TeacherFName = teacherFname;
                NewTeacher.TeacherLName = teacherLname;
                NewTeacher.EmployeeNumber = employeenumber;
                NewTeacher.HireDate = hiredate;
                NewTeacher.Salary = salary;
            }

            return NewTeacher;
        }
    
    }
}
