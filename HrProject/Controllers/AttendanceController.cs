using HrProject.Global;
using HrProject.Models;
using HrProject.Repositories.AttendanceRepository;
using HrProject.Repositories.DepartmentRepo;
using HrProject.Repositories.EmployeeRepo;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace HrProject.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepositary attendanceRepo;
        private readonly IGeneralSettingRepository generalSettingRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IEmployeeRepository employeeRepo;
        public AttendanceController(IEmployeeRepository employeeRepo, IAttendanceRepositary attendanceRepo, IGeneralSettingRepository generalSettingRepository, IDepartmentRepository departmentRepository)
        {
            this.employeeRepo = employeeRepo;
            this.attendanceRepo = attendanceRepo;
            this.generalSettingRepository = generalSettingRepository;
            this.departmentRepository = departmentRepository;
        }

        #region New Attendance

        [Authorize(Permissions.Attendance.View)]
        public IActionResult Index()
        {
            ViewData["Employees"] = employeeRepo.GetAllEmployees();
            ViewData["DeptList"] = departmentRepository.GetAllDepartments();
            var allAttendances = attendanceRepo.GetAll();
            return View(allAttendances);
        }

        [HttpGet]
        [Authorize(Permissions.Attendance.Add)]
        public IActionResult AddAttendance()
        {
            ViewBag.Employees = employeeRepo.GetAllEmployees();
            //ViewData["Employees"] = employeeRepo.GetAllEmployees();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Attendance.Add)]
        public IActionResult AddAttendance(int id, AttendanceViewModel attendance)
        {
            if (ModelState.IsValid == true)
            {
                //=============== General Data ==============

                var empSalary = employeeRepo.GetSalary(attendance.EmpId);
                var startTime = employeeRepo.GetStartTime(attendance.EmpId);
                var endTime = employeeRepo.GetLeaveTime(attendance.EmpId);
                var originWorkingHours = endTime - startTime;

                //============= Actual Data ================

                var actualStartTime = int.Parse(attendance.ArrivalTime.ToString("HH"));
                var actualEndTime = int.Parse(attendance.DepartureTime.ToString("HH"));
                var actualWorkingHours = actualEndTime - actualStartTime;

                //============= Price of Hour from GeneralSetting

                var bounsValue = generalSettingRepository.OverTimePricePerHour();
                var discountValue = generalSettingRepository.DiscountTimePricePerHour();

                if (actualWorkingHours < originWorkingHours)
                {
                    var dicountHours = originWorkingHours - actualWorkingHours;
                    attendance.DiscountHour = dicountHours;
                }
                else if (actualWorkingHours > originWorkingHours)
                {
                    var bounsHours = actualWorkingHours - originWorkingHours;
                    attendance.Bounshour = bounsHours;
                }


                var empAttendance = attendanceRepo.GetById(attendance.EmpId, attendance.Date);
                if (empAttendance != null)
                {
                    TempData["AlertMessage"] = "This employee has already signed in today.";
                    ViewData["Employees"] = employeeRepo.GetAllEmployees();
                    return RedirectToAction("Index");
                }
                attendanceRepo.Add(attendance);
                return RedirectToAction("Index");

            }
            ViewData["DeptList"] = departmentRepository.GetAllDepartments();
            ViewData["Employees"] = employeeRepo.GetAllEmployees();
            var allAttendances = attendanceRepo.GetAll();
            return View("Index", allAttendances);
        }


        [Authorize(Permissions.Attendance.Delete)]
        public IActionResult Delete(int id)
        {
            attendanceRepo.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Permissions.Attendance.View)]
        public IActionResult SearchByEmpName(int? empId, DateTime? targetDate, int? deptId)
        {
            if (empId != null && targetDate == null && deptId == null)
            {
                var allEmpAlltendance = attendanceRepo.GetAllAttendanceByEmployeeId(empId);
                return View(allEmpAlltendance);
            }
            if (targetDate != null && empId != null)
            {
                var allEmpAttendance = attendanceRepo.GetAllAttendanceByEmployeeAndDate(empId, targetDate);
                return View(allEmpAttendance);
            }
            if (targetDate != null && empId == null && deptId == null)
            {
                var allEmpAttendance = attendanceRepo.GetAllAttendanceByDate(targetDate);
                return View(allEmpAttendance);
            }
            if (deptId != null && targetDate == null)
            {
                var allEmpAttendance = attendanceRepo.GetAllAttendanceByDepatment(deptId);
                return View(allEmpAttendance);
            }
            if (deptId != null && targetDate != null)
            {
                var allEmpAttendance = attendanceRepo.GetAllAttendanceByDepartmentAndDate(deptId, targetDate);
                return View(allEmpAttendance);
            }
            return RedirectToAction(nameof(Index));
        }


        #endregion
    }

}

