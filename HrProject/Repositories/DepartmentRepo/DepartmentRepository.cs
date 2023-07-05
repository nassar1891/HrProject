using HrProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.DepartmentRepo
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrContext context;
        public DepartmentRepository(HrContext context)
        {
            this.context = context;
        }
        
        public List<Department> GetAllDepartments()
        {
            return context.Departments.ToList();
        }

        public Department GetDepartmentById(int id)
        {
            return context.Departments.Include(e => e.DeptName).FirstOrDefault(emp => emp.Id == id);
        }
    }
}
