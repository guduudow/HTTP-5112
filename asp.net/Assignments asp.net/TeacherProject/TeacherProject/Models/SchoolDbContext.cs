using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


namespace TeacherProject.Models
{
    public class SchoolDbContext
    {
        //read-only private properties
        //only SchoolDBContext can read them
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }


        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                       + "; user = " + User
                       + "; database = " + Database
                       + "; port = " + Port
                       + "; password = " + Password
                       + "; convert zero datetime = True";
            }
        }

        //actual method to get the database
        ///<summary>
        ///Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext School  = new SchoolDbContext();
        /// MySQLConnection conn = School.AccessDatabase();
        /// </example>
        /// <returns>
        /// A MySQL Connection Object
        /// </returns>
        /// 
        public MySqlConnection AccessDatabase()
        {
            //creating a new instance of the MySqlConnection class to create an object
            //object is a specific connection to the school database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}

