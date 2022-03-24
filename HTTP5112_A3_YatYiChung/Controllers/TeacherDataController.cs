using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HTTP5112_A3_YatYiChung.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace HTTP5112_A3_YatYiChung.Controllers
{
    public class TeacherDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <param name="SearchKey">Search ket (optional) of teacher name</param>
        /// <example>GET api/teacherdata/listteachers </example>
        /// <example>GET api/teacherdata/listteachers/linda </example>
        /// <returns>
        /// A list of teacher object (including fname, id, lname)
        /// </returns>
        [HttpGet]
        [Route("api/teacherdata/listteachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            if(SearchKey != null)
            {
                Debug.WriteLine("The search key is " + SearchKey.ToString());
            }
           
            

            //create an instanse of connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query = "Select * from teachers";

            if (SearchKey != null)
            {
                query = query + " where lower(teacherfname)=lower(@input) OR lower(teacherlname)=lower(@input) OR hiredate LIKE '%(@input)%'";
                cmd.Parameters.AddWithValue("@input", SearchKey);
                cmd.Prepare();
            }

            Debug.WriteLine("the query is :"+query);
            cmd.CommandText = query;

           //executing an sql command
            MySqlDataReader ResultSet = cmd.ExecuteReader();

        //create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>{};

     
            while (ResultSet.Read())
            {

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);
                NewTeacher.HireDate = ResultSet["hiredate"].ToString();




                /*  string TeacherName = ResultSet["teacherid"] + " " +ResultSet["teacherfname"] + " " + ResultSet["teacherlname"] + " " 
                      + ResultSet["employeenumber"] + " " + ResultSet["hiredate"] + " " + ResultSet["salary"];*/

                Teachers.Add(NewTeacher);
            }

    
            Conn.Close();


            return Teachers;
        }

        //Search teach by teacherid
        [HttpGet]
        [Route("api/teacherdata/FindTeacher/{teacherid}")]
        public Teacher FindTeacher(int teacherid)
        {
            //create an instance between the web server and database
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers where teacherid=@inputid ";

            cmd.Parameters.AddWithValue("@inputid", teacherid);
            cmd.Prepare();

            //executing an sql command
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create an empty list of teacher names
            Teacher SelectedTeacher = new Teacher(); 

            //loop through each list of teacher names
            while (ResultSet.Read())
            {

                // Access column information by the DB column names as an index
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);
                SelectedTeacher.HireDate = ResultSet["hiredate"].ToString();


            }


            Conn.Close();


            return SelectedTeacher;
        }

    }
}
