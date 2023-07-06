using HrProject.Models;
using HrProject.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Repositories.AttendanceRepo
{
    public class attendantRepo : IAttendantRepo
    {
        public HrContext Hr { get; }
        public attendantRepo(HrContext hr)
        {
            Hr = hr;
        }

    

      
        public void delete(Attendance attendant)
        {
            Attendance _attendance = GetAttendance(attendant.Id);
            Hr.Remove(_attendance);
        }

        public void update(Attendance attendant)
        {
            Attendance _attendance = GetAttendance(attendant.Id);

            Hr.Entry(attendant).State = EntityState.Modified;
        }

        public List<Attendance> getAll()
        {
            return Hr.Attendances.Include(e=>e.Employee).ToList();
        }

        public Attendance GetAttendance(int id)
        {
          Attendance attendance=  Hr.Attendances.Include(a=>a.Employee).FirstOrDefault(a=>a.Id==id);
            return attendance;
        }

        public void save()
        {
           Hr.SaveChanges();
        }

        public void CreatArrive(Attendance attendant)
        {
            Hr.Add(attendant);
        }

        public void updateLeave(Attendance attendant)
        {
            

            Hr.Entry(attendant).State = EntityState.Modified;

        }

        public Attendance search(string ssn)
        {
            return Hr.Attendances.Include(e => e.Employee).SingleOrDefault(e=>e.Employee.NationalId==ssn);
        }
    }
}
