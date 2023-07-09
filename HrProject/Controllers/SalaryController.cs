using HrProject.Models;
using HrProject.Repositories.AttendanceRepository;
using HrProject.Repositories.EmployeeRepo;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.Repositories.HolidayRepo;
using HrProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HrProject.Controllers
{
	public class SalaryController : Controller
	{
		HrContext context;
		private readonly IEmployeeRepository employeeRepo;
		private readonly IGeneralSettingRepository generalSettingRepo;
		private readonly IAttendanceRepositary attendanceRepo;
		private readonly IWeeklyHolidayRepository weeklyHolidayRepo;

		public SalaryController(IEmployeeRepository employeeRepo, IGeneralSettingRepository generalSettingRepo, IAttendanceRepositary attendanceRepo, IWeeklyHolidayRepository weeklyHolidayRepo)
		{
			this.weeklyHolidayRepo = weeklyHolidayRepo;
			this.employeeRepo = employeeRepo;
			this.generalSettingRepo = generalSettingRepo;
			this.attendanceRepo = attendanceRepo;
		}
		public IActionResult AllReports()
		{

			List<SalaryReportAttendanceVM> salaryReportAttendanceVMs = new List<SalaryReportAttendanceVM>();

			var attendanceList = attendanceRepo.GetAll();
			

			foreach (var attendance in attendanceList)
			{
				SalaryReportAttendanceVM SalaryReportVM = new SalaryReportAttendanceVM();

				int? totalOverTimeHours = 0;
				int? totalDiscountTimeHours = 0;

				var attendanceListForEmployee = attendanceRepo.GetAllAttendanceByEmployee(attendance.Emp_Id, attendance.Date);
				foreach (var item in attendanceListForEmployee)
				{
					if (item.Bounshour != null)
						totalOverTimeHours += item.Bounshour;
					if (item.DiscountHour != null)
						totalDiscountTimeHours += item.DiscountHour;
				}

				var month = attendance.Date.Month;
				var year = attendance.Date.Year;
				int attendanceDays = attendanceRepo.AttendanceDays(attendance.Emp_Id);
				double SalaryPerDay = employeeRepo.GetSalary(attendance.Emp_Id) / DateTime.DaysInMonth(year, month);
				int Holidays = (weeklyHolidayRepo.GetAllHolidays().Select(x => x.Holiday).Count()) * 4;
				int absentDays = DateTime.DaysInMonth(year, month) - (Holidays + attendanceDays);


				int overTimePricePerHour = generalSettingRepo.OverTimePricePerHour();
				int discountTimePricePerHour = generalSettingRepo.DiscountTimePricePerHour();


				var employee = employeeRepo.GetEmployeeById(attendance.Emp_Id);
				SalaryReportVM.EmpName = $"{employee.FirstName} {employee.LastName}";
				SalaryReportVM.DeptName = employee.Department.DeptName;
				SalaryReportVM.Salary = employee.Salary;
				SalaryReportVM.AttendanceDays = attendanceDays;
				SalaryReportVM.AbsentDays = absentDays;
				SalaryReportVM.OverTimePrice = (double)(overTimePricePerHour * totalOverTimeHours);
				SalaryReportVM.DeductionTimePrice = (double)(discountTimePricePerHour * totalDiscountTimeHours);
				SalaryReportVM.total = employee.Salary + (double)(overTimePricePerHour * totalOverTimeHours) - ((double)(discountTimePricePerHour * totalDiscountTimeHours) + (absentDays * SalaryPerDay));


				salaryReportAttendanceVMs.Add(SalaryReportVM);
			}
			ViewData["Employees"] = employeeRepo.GetAllEmployees();
			return View(salaryReportAttendanceVMs.DistinctBy(n=>n.EmpName).ToList());
		}


		public IActionResult Index(int empId, DateTime targetDate)
		{
			List<Employee> employees = employeeRepo.GetAllEmployees();
			List<SalaryReportAttendanceVM> salaryReportAttendanceVMs = new List<SalaryReportAttendanceVM>();
			var attendanceListForEmployee = attendanceRepo.GetAllAttendanceByEmployee(empId, targetDate);
			int? totalOverTimeHours = 0;
			int? totalDiscountTimeHours = 0;
			foreach (var item in attendanceListForEmployee)
			{
				if (item.Bounshour != null)
					totalOverTimeHours += item.Bounshour;
				if (item.DiscountHour != null)
					totalDiscountTimeHours += item.DiscountHour;
			}

			//var attendanceInfo = attendanceRepo.GetAll();
			//foreach( var attendance in attendanceInfo)
			//{
			SalaryReportAttendanceVM SalaryReportVM = new SalaryReportAttendanceVM();

			//int month = int.Parse(targetDate.ToString("mm"));
			//int year = int.Parse(targetDate.ToString("YYYY"));
			var month = targetDate.Month;
			var year = targetDate.Year;
			int attendanceDays = attendanceRepo.AttendanceDays(empId);
			double SalaryPerDay = employeeRepo.GetSalary(empId) / DateTime.DaysInMonth(year, month);
			int Holidays = (weeklyHolidayRepo.GetAllHolidays().Select(x => x.Holiday).Count()) * 4;
			int absentDays = DateTime.DaysInMonth(year, month) - (Holidays + attendanceDays);


			int overTimePricePerHour = generalSettingRepo.OverTimePricePerHour();
			int discountTimePricePerHour = generalSettingRepo.DiscountTimePricePerHour();


			var employee = employeeRepo.GetEmployeeById(empId);
			SalaryReportVM.EmpName = $"{employee.FirstName} {employee.LastName}";
			SalaryReportVM.DeptName = employee.Department.DeptName;
			SalaryReportVM.Salary = employee.Salary;
			SalaryReportVM.AttendanceDays = attendanceDays;
			SalaryReportVM.AbsentDays = absentDays;
			SalaryReportVM.OverTimePrice = (double)(overTimePricePerHour * totalOverTimeHours);
			SalaryReportVM.DeductionTimePrice = (double)(discountTimePricePerHour * totalDiscountTimeHours);
			SalaryReportVM.total = employee.Salary + (double)(overTimePricePerHour * totalOverTimeHours) - ((double)(discountTimePricePerHour * totalDiscountTimeHours) + (absentDays * SalaryPerDay));


			//salaryReportAttendanceVMs.Add(SalaryReportVM);
			// }
			return View(SalaryReportVM);
		}


		#region Old

		//public IActionResult SalaryReport(DateTime targetDate)
		//{
		//    //ViewBag.empId = empId;
		//    ViewBag.datemonth = targetDate;
		//    ViewBag.empolyeeList = employeeRepo.GetAllEmployees();
		//    List<SalaryReportAttendanceVM> ListOfEmployeesSalary = salaryRepo.SalaryReport(targetDate);
		//    //if(/*empId != null &&*/ targetDate != null)
		//    //{
		//    //    List<SalaryReportAttendanceVM> FilterList = salaryRepo.SalaryReport(targetDate);
		//    //    return View(FilterList);

		//    //}
		//    /*   else*/
		//    if (/* empId == null &&*/ targetDate != null)
		//    {
		//        if(targetDate.Year < 2010)
		//        {
		//            ModelState.AddModelError("", "The year should be after 2010");
		//            return View(
		//               new SalaryReportAttendanceVM
		//               {
		//                   filterdate = new dateFormual { Year = targetDate.Year, Month = targetDate.Month }
		//               });

		//        }
		//        else
		//        {
		//            List<SalaryReportAttendanceVM> FilterList = salaryRepo.SalaryReport(targetDate);
		//             return View(FilterList);
		//            //dateFormual filter = new dateFormual { Year = targetDate.Year, Month = targetDate.Month };
		//            //List<SalaryReportAttendanceVM> filteredList = ListOfEmployeesSalary.Where(n => n.filterdate.Month == filter.Month && n.filterdate.Year == filter.Year).ToList();
		//            //return View(filteredList);
		//        }
		//    }
		//    //else if (/*empId != null &&*/ targetDate == null )
		//    //{
		//    //    List<SalaryReportAttendanceVM> filteredList = ListOfEmployeesSalary.Where(n => n.EmpId == empId).ToList();
		//    //    return View(filteredList);
		//    //}


		//    return View(ListOfEmployeesSalary);
		//}

		//public IActionResult EmpolyeeSalaryReport(int empId , int targetM , int targetY)
		//{
		//    dateFormual filterDate = new dateFormual { Month = targetM, Year = targetY};
		//    SalaryReportAttendanceVM empSalary = salaryRepo.EmployeeSalaryReport(empId, filterDate);
		//    return View(empSalary);
		//}
		#endregion

	}
}
