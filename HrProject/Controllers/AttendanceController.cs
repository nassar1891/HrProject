using HrProject.Models;
using HrProject.Repositories.AttendanceRepository;
using HrProject.Repositories.EmployeeRepo;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HrProject.Controllers
{
	public class AttendanceController : Controller
	{
		private readonly IAttendanceRepositary attendanceRepo;
		private readonly IGeneralSettingRepository generalSettingRepository;
		private readonly IEmployeeRepository employeeRepo;
		public AttendanceController(IEmployeeRepository employeeRepo, IAttendanceRepositary attendanceRepo, IGeneralSettingRepository generalSettingRepository)
		{
			this.employeeRepo = employeeRepo;
			this.attendanceRepo = attendanceRepo;
			this.generalSettingRepository = generalSettingRepository;
		}

		#region New Attendance
		public IActionResult Index()
		{
			ViewData["Employees"] = employeeRepo.GetAllEmployees();
			var allAttendances = attendanceRepo.GetAll();
			return View(allAttendances);
		}

		[HttpGet]
		public IActionResult AddAttendance()
		{
			ViewBag.Employees = employeeRepo.GetAllEmployees();
			//ViewData["Employees"] = employeeRepo.GetAllEmployees();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
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


				var empAttendance = attendanceRepo.GetById(attendance.EmpId,attendance.Date);
				if (empAttendance != null)
				{
					TempData["AlertMessage"] = "This employee has already signed in today.";
					ViewData["Employees"] = employeeRepo.GetAllEmployees();
					return RedirectToAction("Index");
				}
				attendanceRepo.Add(attendance);
				return RedirectToAction("Index");
				
			}

			ViewData["Employees"] = employeeRepo.GetAllEmployees();
			var allAttendances = attendanceRepo.GetAll();
			return View("Index", allAttendances);
		}

		public IActionResult Delete(int id)
		{
			attendanceRepo.Delete(id);
			return RedirectToAction("Index");
		}

		//public IActionResult Check(DateTime ArrivalTime,DateTime DepartureTime)
		//{
		//	if (DepartureTime > ArrivalTime)
		//	{
		//		return Json(true);
		//	}
		//	return Json(false);
		//}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddAttendance(int id, Attendance attendance)
        //{
        //	if (ModelState.IsValid == true)
        //	{
        //		if (attendance.DepartureTime != null)
        //		{

        //			//=============== General Data ==============

        //			var empSalary = employeeRepo.GetSalary(attendance.Emp_Id);
        //			var startTime = employeeRepo.GetStartTime(attendance.Emp_Id);
        //			var endTime = employeeRepo.GetLeaveTime(attendance.Emp_Id);
        //			var originWorkingHours = endTime - startTime;

        //			//============= Actual Data ================

        //			var actualStartTime = int.Parse(attendance.ArrivalTime?.ToString("HH"));
        //			var actualEndTime = int.Parse(attendance.DepartureTime?.ToString("HH"));
        //			var actualWorkingHours = actualEndTime - actualStartTime;

        //			//============= Price of Hour from GeneralSetting

        //			var bounsValue = generalSettingRepository.OverTimePricePerHour();
        //			var discountValue = generalSettingRepository.DiscountTimePricePerHour();

        //			if (actualWorkingHours < originWorkingHours)
        //			{
        //				var dicountHours = originWorkingHours - actualWorkingHours;
        //				attendance.DiscountHour = dicountHours;
        //			}
        //			else if (actualWorkingHours > originWorkingHours)
        //			{
        //				var bounsHours = actualWorkingHours - originWorkingHours;
        //				attendance.Bounshour = bounsHours;
        //			}
        //		}
        //		var flag = 0;
        //		if (attendanceRepo.GetById(id) == null)
        //		{
        //			var allAttendance = attendanceRepo.GetAll();
        //			if (allAttendance != null)
        //			{
        //				foreach (var item in allAttendance)
        //				{
        //					if (attendance.Emp_Id == item.Emp_Id && attendance.Date == item.Date)
        //					{
        //						flag = 1;

        //					}
        //				}
        //			}
        //			if (flag == 0)
        //			{
        //				attendanceRepo.Add(attendance);
        //			}
        //			else
        //			{
        //				ModelState.AddModelError(string.Empty, "This Employee Alredy Attended Today");
        //				ViewData["Employees"] = employeeRepo.GetAllEmployees();
        //				return View();
        //			}
        //		}
        //		ViewData["Employees"] = employeeRepo.GetAllEmployees();
        //		attendanceRepo.Update(id, attendance);
        //		return RedirectToAction("Index");

        //	}

        //	ViewData["Employees"] = employeeRepo.GetAllEmployees();
        //	var allAttendances = attendanceRepo.GetAll();
        //	return View("Index", allAttendances);
        //}


        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //	var attendance = attendanceRepo.GetById(id);
        //	ViewData["Employees"] = employeeRepo.GetAllEmployees();
        //	return View("AddAttendance");
        //}

    }




	#endregion






	#region Old

	//public IActionResult index()
	//{
	//    List<Attendance> attendances = attendanceRepo.getAll();
	//    return View(attendances);
	//}

	//public IActionResult search(string SSN)
	//{
	//    Attendance attendances = attendanceRepo.search(SSN);
	//    return View("search", attendances);
	//}

	//public IActionResult createArrive()
	//{
	//    createArriveAttendenceViewModel createArriveAttendence = new createArriveAttendenceViewModel();
	//    createArriveAttendence.employees = employeeRepo.GetAllEmployees();
	//    return View("createArrive", createArriveAttendence);
	//}
	//[HttpPost]
	//[ValidateAntiForgeryToken]
	//public IActionResult createArrive(createArriveAttendenceViewModel arrive)
	//{
	//    Attendance attendance = new Attendance()
	//    {
	//        Date = DateTime.Now,
	//        ArrivalTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm"),
	//        Emp_Id = arrive.Emp_Id,
	//    };
	//    attendanceRepo.CreatArrive(attendance);
	//    attendanceRepo.save();

	//    return RedirectToAction("index");
	//}
	//public IActionResult details(int id)
	//{
	//    Attendance attendance = attendanceRepo.GetAttendance(id);
	//    return View("details", attendance);
	//}
	//public IActionResult createLeave(int id)
	//{
	//    Attendance attendance = attendanceRepo.GetAttendance(id);

	//    return View("createLeave", attendance);
	//}
	//[HttpPost]
	//[ValidateAntiForgeryToken]
	//public IActionResult createLeave(Attendance attendance)
	//{

	//    attendance.DepartureTime = DateTime.Now.TimeOfDay.ToString("hh\\:mm");
	//    attendanceRepo.updateLeave(attendance);
	//    attendanceRepo.save();
	//    return RedirectToAction("index");
	//}
	//public IActionResult delete(int id)
	//{
	//    Attendance attendance = attendanceRepo.GetAttendance(id);
	//    return View("delete", attendance);
	//}
	//[HttpPost]
	//public IActionResult delete(Attendance attendance)
	//{
	//    attendanceRepo.delete(attendance);
	//    attendanceRepo.save();
	//    return RedirectToAction("index");
	//}
	#endregion

}

