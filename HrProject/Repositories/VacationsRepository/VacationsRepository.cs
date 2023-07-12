using HrProject.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HrProject.ViewModels;

namespace HrProject.Repositories.VacationsRepository

{
    public class VacationsRepository : IVacationsRepository
    {

        HrContext context;
        public VacationsRepository(HrContext context)
        {
            this.context = context;

        }

        public void Create(VacationsViewModel vac)
        {
            var VacationDb = new Vacations()
            {
                Name = vac.Name,
                Date = vac.Date,
            };
            context.Vacations.Add(VacationDb);
            context.SaveChanges();


        }






        public void Delete(int id)
        {
            Vacations delVacations = GetById(id);
            context.Vacations.Remove(delVacations);
        }

        public List<Vacations> GetAll()
        {
            return context.Vacations.ToList();
        }

        public Vacations GetById(int id)
        {
            return context.Vacations.FirstOrDefault(e => e.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void update(int id, VacationsViewModel vac)
        {
            Vacations updatevacations = GetById(id);
            updatevacations.Name = vac.Name;
            updatevacations.Date = vac.Date;
        }
    }
}
