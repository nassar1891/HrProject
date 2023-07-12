using HrProject.Models;
using HrProject.ViewModels;

namespace HrProject.Repositories.AttendanceRepository
{
    public interface IAttendanceRepositary
    {

        List<Attendance> GetAll();
        List<Attendance> GetAllAttendanceByEmployee(int? empId,DateTime targetDate);
        List<Attendance> GetAllAttendanceByEmployeeAndDate(int? empId,DateTime? targetDate);
        List<Attendance> GetAllAttendanceByDepatment(int? deptId);
        List<Attendance> GetAllAttendanceByDate(DateTime? targetDate);
        List<Attendance> GetAllAttendanceByDepartmentAndDate(int? deptId,DateTime? targetDate);
        List<Attendance> GetAllAttendanceByEmployeeId(int? empId);
        void Add(AttendanceViewModel attendance);
        void Delete(int id);
        Attendance GetById(int? id,DateTime todayDate);
        void Update(int id, Attendance attendance);

        int AttendanceDays(int? empId,DateTime targetDate);
        int AttendanceDays(int? empId);

        decimal? OverHoursSum(int? empId);
        decimal? DiscountHoursSum(int? empId);

        //int GetArrivalTime(int empId);

        #region OLd
        //void CreatArrive(Attendance attendant);
        //void updateLeave(Attendance attendant);
        //void update(Attendance attendant);
        //void delete(Attendance attendant);
        //Attendance GetAttendance(int id);
        //List<Attendance> getAll();
        ////List<dateFormual> GetDateFormuals();

        //Attendance search(string ssn);
        //void save();

        #endregion
    }
}
