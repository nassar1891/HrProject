using HrProject.Models;
using HrProject.ViewModels;

namespace HrProject.Repositories.AttendanceRepo
{
    public interface IAttendantRepo
    {
        void CreatArrive(Attendance attendant);
        void updateLeave(Attendance attendant);
        void update(Attendance attendant);
        void delete(Attendance attendant);
        Attendance GetAttendance(int id);
        List<Attendance> getAll();
        Attendance search(string ssn);
        void save();
    }
}
