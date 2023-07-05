using HrProject.Models;

namespace HrProject.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        Department GetDepartmentById(int id);
        public List<Department> GetAllDepartments();
    }
}
