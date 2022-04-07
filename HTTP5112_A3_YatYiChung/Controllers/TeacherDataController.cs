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

        ///This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of teachers in the system
        /// </summary>
        /// <param name="SearchKey">Search key (optional) of teacher name</param>
        /// <example>GET api/teacherdata/listteachers </example>
        /// <example>GET api/teacherdata/listteachers/linda </example>
        /// <example>GET api/teacherdata/listteachers/morris </example>
        /// <example>GET api/teacherdata/listteachers/20160806 </example>
        /// <example>GET api/teacherdata/listteachers/55 </example> 
        /// <returns>
        /// A list that display the information (employee number, hire date, salary, first name, teacher id, last name) of the teachers
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

            //specific sql query to filter the input
            //determine what kind of information is allow to use as the SearchKey
            if (SearchKey != null)
            {
                query = query + " where lower(teacherfname)=lower(@input) OR lower(teacherlname)=lower(@input) OR hiredate=(@input)";
                /// ATTEMPT for salary
                /// I wanted to inlcude the sql query at the end of the above sql script to search by salary using -> OR salary LIKE '%"+"(@input)"+"%'"
                /// The reason why I use LIKE instead of = is because the search input cannot have a decimal, it will causes an error in the url,
                /// therefore if the input is api/teacherdata/listteachers/55, the output will show any teacher with the salaru that have the int 55 in it.
                /// Howwever, I am unsure why this query doesn't work in this API controller. When I uses the same sql query  -> OR salary LIKE '%"+"(@input)"+"%'" in the sql script on phpmyadmin, it display the correct output. 
                cmd.Parameters.AddWithValue("@input", SearchKey);
                cmd.Prepare();
            }

            Debug.WriteLine("the query is :"+query);
            cmd.CommandText = query;

           //executing an sql command
            MySqlDataReader ResultSet = cmd.ExecuteReader();

           //create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>{};

            //A loop that runs through the list
            while (ResultSet.Read())
            {
                // Access column information by the DB column names as an index
                //Display the information of the teachers 
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
               // NewTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);
                NewTeacher.HireDate = ResultSet["hiredate"].ToString();


                Teachers.Add(NewTeacher);
            }

    
            Conn.Close();


            return Teachers;
        }

        //Search teacher by teacherid
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


            //A loop that runs through the list and find the individual(s) that matches the input
            while (ResultSet.Read())
            {

                // Access column information by the DB column names as an index
                // Display the information of an individual(s) 
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                //SelectedTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);
                SelectedTeacher.HireDate = ResultSet["hiredate"].ToString();


            }


            Conn.Close();


            return SelectedTeacher;
        }

        /// <summary>
        /// Add a new teacher to the system given teacher information
        /// </summary>
        /// <paramref name="NewTeacher"/> The teacher information I want to add
        /// 
        public void AddTeacher(Teacher NewTeacher)
        {
            //create an instanse of connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate) values (@teacherfname,@teacherlname,@employeenumber,CURRENT_DATE())";

            cmd.CommandText= query;

            cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);

            cmd.Prepare();
                                       

            cmd.ExecuteNonQuery();

            Conn.Close ();
        }

        /// <summary>
        /// Delete a teacher in the system
        /// </summary>
        /// <param name="TeacherId"> The primary key teacherid </param>

        public void DeleteTeacher(int TeacherId)
        {
            //create an instanse of connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id",TeacherId);
            cmd.CommandText = query;
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close(); 
        }

    }
}
