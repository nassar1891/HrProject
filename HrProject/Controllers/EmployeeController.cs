using HrProject.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrProject.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
        [Authorize(Permissions.Employee.View)]
        public ActionResult Index()
        {
            return View();
        }

		// GET: EmployeeController/Details/5
		[Authorize(Permissions.Employee.View)]
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        //[Authorize(Permissions.Employee.Add)]
        [Authorize(Permissions.Employee.Add)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Permissions.Employee.Add)]
		public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        [Authorize(Permissions.Employee.Edit)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Permissions.Employee.Edit)]
		public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        [Authorize(Permissions.Employee.Delete)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Permissions.Employee.Delete)]
		public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
