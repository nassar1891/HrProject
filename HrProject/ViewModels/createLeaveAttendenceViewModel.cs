using HrProject.Models;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> d2de3bdbe40d22c357e33818d686dbde43129574

namespace HrProject.ViewModels
{
    public class createLeaveAttendenceViewModel
    {
<<<<<<< HEAD
=======
       
>>>>>>> d2de3bdbe40d22c357e33818d686dbde43129574
        public DateTime Date { get; set; }
        public string? DepartureTime { get; set; }
        public string? ArrivalTime { get; set; }
        public int Emp_Id { get; set; }

        public virtual Employee? Employee { get; set; } = null!;
    }
}
