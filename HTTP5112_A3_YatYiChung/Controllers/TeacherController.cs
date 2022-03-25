using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5112_A3_YatYiChung.Models;
using System.Diagnostics;

namespace HTTP5112_A3_YatYiChung.Controllers
{
    /// <summary>
    /// Connecting the API controller - TeacherDataController.cs with Views/Teacher/List.cshtml and Views/Teacher/Show.cshtml
    /// </summary>
    /// <param name="SearchKey">To look up a specific teacher</param>
    /// <param name="id">To look up a specific teacher with teacherid</param>
    /// <example>GET teacher/list </example>
    /// <example>GET teacher/show/2 </example>
    /// <returns>
    /// 
    /// </returns>


    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        //Get: Teacher/List
        //showing a page of all authors in the system

        [Route("Teacher/List/{SearchKey}")]
        public ActionResult List(string SearchKey)
        {

            //debugging message to see if we have gathererd the key
            Debug.WriteLine("The key is " + SearchKey);

            //connect to our data access layer
            //get our teachers
            //pass the teachers to the view Teacher/List.cshtml with the parameter SearchKey
            TeacherDataController controller = new TeacherDataController();
            IEnumerable <Teacher> Teacher= controller.ListTeachers(SearchKey); 

            return View(Teacher);


        }

        public ActionResult Show(int id)
        {
            //connect to our data access layer
            //pass the SelectedTeacher to Teacher/Show.cshtml with the parameter id

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);



            //route the single teacher info to show.cshtml
            return View(SelectedTeacher);
        }


    }
}