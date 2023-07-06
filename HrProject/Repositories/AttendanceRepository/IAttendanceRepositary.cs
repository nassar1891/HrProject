using HrProject.Models;
using HrProject.ViewModels;

namespace HrProject.Repositories.AttendanceRepository
{
    public interface IAttendanceRepositary
    {
        void CreatArrive(Attendance attendant);
        void updateLeave(Attendance attendant);
        void update(Attendance attendant);
        void delete(Attendance attendant);
        Attendance GetAttendance(int id);
        List<Attendance> getAll();
        //List<dateFormual> GetDateFormuals();

        Attendance search(string ssn);
        void save();
    }
}
