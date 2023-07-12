using HrProject.Models;
using HrProject.ViewModels;

namespace HrProject.Repositories.VacationsRepository
{
    public interface IVacationsRepository
    {
        List<Vacations> GetAll();
        Vacations GetById(int id);
        void Create(VacationsViewModel vac);
        void Save();
        void Delete(int id);
        public void update(int id, VacationsViewModel vac);

    }
}
