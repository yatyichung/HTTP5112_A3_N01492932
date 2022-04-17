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

        //Get: /Teacher/Show/{id}
        //[Route("/Teacher/Show/{TeacherId}")]
        public ActionResult Show(int id)
        {
            //connect to our data access layer
            //pass the SelectedTeacher to Teacher/Show.cshtml with the parameter id

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);



            //route the single teacher info to show.cshtml
            return View(SelectedTeacher);
        }



        //Get: /Teacher/DeleteConfirm/{id}
        //[Route("/Teacher/DeleteConfirm/{TeacherId}")]
        public ActionResult DeleteConfirm(int id)
        {
            //connect to our data access layer
            //pass the SelectedTeacher to Teacher/Show.cshtml with the parameter id

            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);



            //route the single teacher info to show.cshtml
            return View(SelectedTeacher);
        }



        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");

        }







        //Get: /Teacher/Add
        public ActionResult Add()
        {
            return View();
        }


        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber) 
        {
            Debug.WriteLine("the teacher info is :" + teacherfname + " " + teacherlname + " " + employeenumber);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = teacherfname;
            NewTeacher.TeacherLName = teacherlname;
            NewTeacher.EmployeeNumber = employeenumber;


            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            //we want to do the following:
            //connect to a database
            //insert into teachers
            // with provided values

            //redirect immediately to the list view
            return RedirectToAction("List");
        }

        /// <summary>
        /// returns a webpage of the teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //GET : /Teacher/Edit/{id}
        public ActionResult Edit(int id)
        {
            //need to pass teacher info to the view to show that to the user
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// this method actually updates the teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //POST : /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLName, string EmployeeNumber)
        {
            Debug.WriteLine("The teacher name is " + TeacherFname);
            Debug.WriteLine("The ID is " + id);

            Teacher TeacherInfo = new Teacher();

            TeacherInfo.TeacherFName = TeacherFname;
            TeacherInfo.TeacherLName = TeacherLName;
            TeacherInfo.EmployeeNumber = EmployeeNumber;


            //update the teacher informaiton
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            //return to the author I just changed
            return RedirectToAction("Show/" + id);
        }


    }
}