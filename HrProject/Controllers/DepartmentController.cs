using HrProject.Global;
using HrProject.Repositories.EmployeeRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using HrProject.Repositories.DepartmentRepo;
using HrProject.ViewModels;

namespace HrProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [Authorize(Permissions.Department.View)]
        public ActionResult Index()
        {
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.DepartmentList = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.Id.ToString()
            });

            return View(departments);
        }


        [Authorize(Permissions.Department.View)]
        public ActionResult Details(int id)
        {
            var dep = _departmentRepository.GetDepartmentById(id);
            return View(dep);
        }


        [Authorize(Permissions.Department.Add)]
        public ActionResult Create()
        {
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.DepartmentList = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.Id.ToString()
            });

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Department.Add)]
        public ActionResult Create(IFormCollection collection)
        {
            var newdep = new Models.Department
            {
                DeptName = collection.FirstOrDefault(x => x.Key == "DeptName").Value,
            };
            _departmentRepository.Insert(newdep);

            return RedirectToAction("Index");
        }


        [Authorize(Permissions.Department.Edit)]
        public ActionResult Edit(int id)
        {
            var dep = _departmentRepository.GetDepartmentById(id);
            var departments = _departmentRepository.GetAllDepartments();
            ViewBag.DepartmentList = departments.Select(x => new SelectListItem
            {
                Text = x.DeptName,
                Value = x.Id.ToString(),
                Selected = x.Id == dep.Id
            });
            var depVM = new DepartmentViewModel
            {
                DeptName = dep.DeptName,
            };
            return View(depVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Department.Edit)]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var newdep = new Models.Department
            {
                DeptName = collection.FirstOrDefault(x => x.Key == "DeptName").Value,
            };
            _departmentRepository.Update(id, newdep);
            return RedirectToAction("Index");
        }

        [Authorize(Permissions.Department.Delete)]
        public ActionResult Delete(int id)
        {
            _departmentRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
