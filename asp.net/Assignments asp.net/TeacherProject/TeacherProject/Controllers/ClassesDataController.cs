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
    public class ClassesDataController : ApiController
    {
        //database class which allows me to connect to my database
        private SchoolDbContext School = new SchoolDbContext();

        //this controller will allow access to a list of classes in the school
        ///<summary>
        ///Returns a list of classes in the system
        /// </summary>
        /// <example>
        /// GET/api/ClassData/ListClasses
        /// </example>
        [HttpGet]
        public IEnumerable<Classes> ListClasses()
        {
            //create connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new query for the database
            MySqlCommand command = conn.CreateCommand();
            //SQL Query goes here
            command.CommandText = "SELECT * FROM CLASSES";
            //Gather result set of query into a variable
            MySqlDataReader reader = command.ExecuteReader();
            //create an empty list of teachers
            List<Classes> ClassesData = new List<Classes>();
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
                //string TeacherInfo = reader["teacherid"] + " " + reader["teacherfname"] + " " + reader["teacherlname"] + " " + reader["employeenumber"] + " " + reader["hiredate"] + " " + reader["salary"];

                Classes NewClass = new Classes();
                NewClass.ClassID = classId;
                NewClass.ClassCode = classcode;
                NewClass.TeacherID = teacherId;
                NewClass.StartDate = startdate;
                NewClass.FinishDate = finishdate;
                NewClass.ClassName = classname;
                //add data to the list
                ClassesData.Add(NewClass);
            }
            //close the connection
            conn.Close();
            //return the final list of teachers
            return ClassesData;
        }

        ///<summary>
        ///Find a class in the system given an id
        /// </summary>
        /// <param name="id">The class primary key</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        public Classes FindClasses(int id)
        {
            Classes NewClass = new Classes();

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


                NewClass.ClassID = classId;
                NewClass.ClassCode = classcode;
                NewClass.TeacherID = teacherId;
                NewClass.StartDate = startdate;
                NewClass.FinishDate = finishdate;
                NewClass.ClassName = classname;
            }

            return NewClass;
        }
    }
}
