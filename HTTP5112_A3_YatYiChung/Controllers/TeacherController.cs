using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5112_A3_YatYiChung.Models;

namespace HTTP5112_A3_YatYiChung.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        //Get: Teacher/List
        //showing a page of all authors in the system
        public ActionResult List()
        {

            //connect to our data access layer
            //get our teachers
            //pass the teachers to the view Teacher/List.cshtml
            TeacherDataController controller = new TeacherDataController();
            IEnumerable <Teacher> Teacher= controller.ListTeachers(); 

            return View(Teacher);


        }

        //Get: /Teacher/Show/{id}
        //[Route("/Teacher/Show/{TeacherId}")]
        public ActionResult Show(int id)
        {

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacherById(id);



            //route the single teacher info to show.cshtml
            return View(SelectedTeacher);
        }


    }
}