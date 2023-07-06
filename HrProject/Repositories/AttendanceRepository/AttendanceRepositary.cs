using HrProject.Models;
using HrProject.Repositories.EmployeeRepo;
using HrProject.Repositories.GeneralSettingRepo;
using HrProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.AttendanceRepository
{
    public class AttendanceRepositary: IAttendanceRepositary
    {
        private readonly HrContext context;
     

        public AttendanceRepositary(HrContext context)
        {
            this.context = context;
          
        }
        public void delete(Attendance attendance)
        {
            Attendance _attendance = GetAttendance(attendance.Id);
            context.Remove(_attendance);
        }

        public void update(Attendance attendace)
        {
            Attendance _attendance = GetAttendance(attendace.Id);


            context.Entry(attendace).State = EntityState.Modified;
        }

        public List<Attendance> getAll()
        {
            return context.Attendances.Include(e => e.Employee).ToList();
        }

        public Attendance GetAttendance(int id)
        {
            Attendance attendance = context.Attendances.Include(a => a.Employee).FirstOrDefault(a => a.Id == id);
            return attendance;
        }

        public void save()
        {
            context.SaveChanges();
        }

        public void CreatArrive(Attendance attendant)
        {
            context.Add(attendant);
        }

        public void updateLeave(Attendance attendant)
        {


            context.Entry(attendant).State = EntityState.Modified;

        }

        public Attendance search(string ssn)
        {
            return context.Attendances.Include(e => e.Employee).SingleOrDefault(e => e.Employee.NationalId == ssn);
        }

        //public List<dateFormual> GetDateFormuals()
        //{
        //    return context.Attendances.Select(x => new dateFormual { Month = x.Date.Month, Year = x.Date.Year }).Distinct().ToList();
        //}


    }
}
