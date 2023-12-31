﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeacherProject.Models;
using MySql.Data.MySqlClient;
using ZstdSharp.Unsafe;
using System.Web.Http.Cors;

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
        /// <returns>
        /// A list of Teacher Objects with fields mapped to the database column values (first name, last name, employee number...)
        /// </returns>
        /// <example>
        /// GET/api/TeacherData/ListTeachers -> {Teacher Object, Teacher Object, Teacher Object...}
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //create connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection
            conn.Open();

            //establish a new query for the database
            MySqlCommand command = conn.CreateCommand();
            //SQL Query goes here
            command.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";
            command.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            command.Prepare();
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
        /// <returns>A teacher object containing information about the teacher with a matching ID. Empty Teacher Object if the ID does not match any teachers in the system.</returns>
        /// <example>api/TeacherData/FindTeacher/6 -> {Teacher Object}</example>
        /// <example>api/TeacherData/FindTeacher/10 -> {Teacher Object}</example>
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
            cmd.CommandText = "SELECT * FROM TEACHERS WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

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


        ///<summary>
        ///Deletes a teacher from the connected mySQL Database if the ID of that author exists.
        /// </summary>
        /// <param name="id">The ID of the teacher</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //create an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection between the web server and database
            conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection
            conn.Close();
        }

        ///<summary>
        ///adds a teacher to the mySQL database
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "TeacherFname": Ederes
        ///     "TeacherLname": Gure
        ///     "EmployeeNumber":T304
        ///     "HireDate": 2023-11-25 00:00:00
        ///     "Salary": 81.00
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //create an instance of a connection
            MySqlConnection conn = School.AccessDatabase();

            //open the connection between the web server and database
            conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber,@HireDate,@Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //close the connection
            conn.Close();
        }

        ///<summary>
        ///Updates a teacher on the MySQL Database. Non-deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to column of the teacher table</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/209
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "TeacherFname": Ederes
        ///     "TeacherLname": Gure
        ///     "EmployeeNumber": T304
        ///     "HireDate": 2024-01-20 00:00:00
        ///     "Salary": 70.00
        /// }
        /// </example>
        /// 
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //create a conenction
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection
            Conn.Open();

            //estbalish a new query
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary=@Salary where teacherid=@TeacherID";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherID", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

    }
}
