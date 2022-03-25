using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace HTTP5112_A3_YatYiChung.Models
{
    public class SchoolDbContext
    {
        /// <summary>
        /// Connection between the SQL database to C#
        /// </summary>
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
 
        public MySqlConnection AccessDatabase()
        {
         
            return new MySqlConnection(ConnectionString);
        }





    }
}