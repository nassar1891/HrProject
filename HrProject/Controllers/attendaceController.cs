using HrProject.Models;
using HrProject.Repositories.AttendanceRepo;
using HrProject.Repositories.EmployeeRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HrProject.Controllers
{
    public class attendaceController : Controller
    {
        public IAttendantRepo Attendant_Repo { get; }
        public IEmployeeRepository Employee_Repo { get; }

        public attendaceController(IAttendantRepo attendant_repo,IEmployeeRepository employee_repo)
        {
            Attendant_Repo = attendant_repo;
            Employee_Repo = employee_repo;
        }

        public IActionResult index()
        {
            List<Attendance> attendances = Attendant_Repo.getAll();
        return View(attendances);
        }


        public IActionResult search(string SSN)
        {
            Attendance attendances = Attendant_Repo.search(SSN);
            return View("search",attendances);
        }

        public IActionResult createArrive()
        {
            createArriveAttendenceViewModel createArriveAttendence=new createArriveAttendenceViewModel();
           createArriveAttendence.employees=Employee_Repo.GetAllEmployees();
            return View("createArrive", createArriveAttendence);
        }
        [HttpPost]
        public IActionResult createArrive(createArriveAttendenceViewModel arrive)
        {
            Attendance attendance = new Attendance()
            {
                Date = DateTime.Now,
                ArrivalTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm"),
                Emp_Id =arrive.Emp_Id,
            };
            Attendant_Repo.CreatArrive(attendance);
            Attendant_Repo.save();
            
            return RedirectToAction("index");
        }

        //public IActionResult createLeave(DateTime Date, int Emp_Id)
        //{

        //    Attendance _attendance = Attendant_Repo.GetAttendance(Date, Emp_Id);


        //    return View("createLeave", _attendance);
        //}

        //[HttpPost]
        //public IActionResult createLeave(Attendance leave)
        //{



        //    Attendance attendance = new Attendance()
        //    {
        //        DepartureTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm"),


        //    };
        //    Attendant_Repo.updateLeave(attendance);
        //    Attendant_Repo.save();
        //    return RedirectToAction("index");
        //}

        public IActionResult details(int id)
        {
            Attendance attendance=Attendant_Repo.GetAttendance(id);
            return View("details",attendance);
        }
        public IActionResult createLeave(int id)
        {
            Attendance attendance = Attendant_Repo.GetAttendance(id);

            return View("createLeave", attendance);
        }
        [HttpPost]
        public IActionResult createLeave(Attendance attendance)
        {
          
        attendance.DepartureTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm");
            Attendant_Repo.updateLeave(attendance);
            Attendant_Repo.save();
            return RedirectToAction("index");
        }
        public IActionResult delete(int id)
        {
            Attendance attendance= Attendant_Repo.GetAttendance(id);
            return View("delete", attendance);
        }
        [HttpPost]
        public IActionResult delete(Attendance attendance)
        {
            Attendant_Repo.delete(attendance);
            Attendant_Repo.save();
            return RedirectToAction("index");
        }
    }
}
