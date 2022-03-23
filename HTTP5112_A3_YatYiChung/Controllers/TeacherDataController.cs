using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5112_A3_YatYiChung.Models;
using MySql.Data.MySqlClient;

namespace HTTP5112_A3_YatYiChung.Controllers
{
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <example>GET api/teacherdata/listteacher</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<string> ListTeacheres()
        {
            
            MySqlConnection Conn = School.AccessDatabase();

        
            Conn.Open();

            
            MySqlCommand cmd = Conn.CreateCommand();

           
            cmd.CommandText = "Select * from Teachers";

           
            MySqlDataReader ResultSet = cmd.ExecuteReader();

       
            List<String> TeacherNames = new List<string>{};

     
            while (ResultSet.Read())
            {
           
                string TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
         
                TeacherNames.Add(TeacherName);
            }

    
            Conn.Close();


            return TeacherNames;
        }


        [HttpGet]
        [Route("api/teacherdata/findteacher/{teacherid}")]
        public string FindTeacher(int teacherid)
        {
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();


            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "Select * from teachers where teacherid ="+teacherid;


            MySqlDataReader ResultSet = cmd.ExecuteReader();


            String TeacherName = "";


            while (ResultSet.Read())
            {

                TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];

            }


            Conn.Close();


            return TeacherName;
        }



    }
}
