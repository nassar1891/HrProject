using HrProject.Models;
using HrProject.Repositories.AttendanceRepository;
using HrProject.Repositories.EmployeeRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HrProject.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepositary attendanceRepo;
        private readonly IEmployeeRepository employeeRepo;
        public AttendanceController(IEmployeeRepository employeeRepo , IAttendanceRepositary attendanceRepo)
        {
            this.employeeRepo = employeeRepo;
            this.attendanceRepo = attendanceRepo;
        }
      
        public IActionResult index()
        {
            List<Attendance> attendances = attendanceRepo.getAll();
            return View(attendances);
        }

        public IActionResult search(string SSN)
        {
            Attendance attendances = attendanceRepo.search(SSN);
            return View("search", attendances);
        }

        public IActionResult createArrive()
        {
            createArriveAttendenceViewModel createArriveAttendence = new createArriveAttendenceViewModel();
            createArriveAttendence.employees = employeeRepo.GetAllEmployees();
            return View("createArrive", createArriveAttendence);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult createArrive(createArriveAttendenceViewModel arrive)
        {
            Attendance attendance = new Attendance()
            {
                Date = DateTime.Now,
                ArrivalTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm"),
                Emp_Id = arrive.Emp_Id,
            };
            attendanceRepo.CreatArrive(attendance);
            attendanceRepo.save();

            return RedirectToAction("index");
        }
        public IActionResult details(int id)
        {
            Attendance attendance = attendanceRepo.GetAttendance(id);
            return View("details", attendance);
        }
        public IActionResult createLeave(int id)
        {
            Attendance attendance = attendanceRepo.GetAttendance(id);

            return View("createLeave", attendance);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult createLeave(Attendance attendance)
        {

            attendance.DepartureTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm");
            attendanceRepo.updateLeave(attendance);
            attendanceRepo.save();
            return RedirectToAction("index");
        }
        public IActionResult delete(int id)
        {
            Attendance attendance = attendanceRepo.GetAttendance(id);
            return View("delete", attendance);
        }
        [HttpPost]
        public IActionResult delete(Attendance attendance)
        {
            attendanceRepo.delete(attendance);
            attendanceRepo.save();
            return RedirectToAction("index");
        }
    }
}
