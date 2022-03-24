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
        /// A list of teacher object (including fname, id, lname)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            
            MySqlConnection Conn = School.AccessDatabase();

        
            Conn.Open();

            
            MySqlCommand cmd = Conn.CreateCommand();

           
            cmd.CommandText = "Select * from Teachers ORDER BY teacherid";

           
            MySqlDataReader ResultSet = cmd.ExecuteReader();

       
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
        [Route("api/teacherdata/findteacherbyid/{teacherid}")]
        public Teacher FindTeacherById(int teacherid)
        {
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();


            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "Select * from teachers where teacherid ="+teacherid;


            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Teacher SelectedTeacher = new Teacher(); 


            while (ResultSet.Read())
            {

                // TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
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

        //Search teacher by teacherfname + teacherlname
        [HttpGet]
        [Route("api/teacherdata/FindTeacherByName/{teacherfname}/{teacherlname}")]
        public Teacher FindTeacherByName(string teacherfname,string teacherlname)
        {
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();


            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "Select * from teachers where teacherfname LIKE '"+teacherfname+ "' AND teacherlname LIKE '"+teacherlname+"'";


            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Teacher SelectedTeacher = new Teacher();


            while (ResultSet.Read())
            {

                // TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
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




        //Search teacher by hiredate
        [HttpGet]
        [Route("api/teacherdata/FindTeacherByhiredate/{hiredate}")]
        public Teacher FindTeacherByHireDate(string hiredate)
        {
            MySqlConnection Conn = School.AccessDatabase();


            Conn.Open();


            MySqlCommand cmd = Conn.CreateCommand();


            cmd.CommandText = "Select * from teachers where hiredate LIKE '%" + hiredate + "%'";


            MySqlDataReader ResultSet = cmd.ExecuteReader();


            Teacher SelectedTeacher = new Teacher();


            while (ResultSet.Read())
            {

                // TeacherName = ResultSet["teacherfname"] + " " + ResultSet["teacherlname"];
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
