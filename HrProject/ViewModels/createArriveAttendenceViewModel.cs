using HrProject.Models;

namespace HrProject.ViewModels
{
    public class createArriveAttendenceViewModel
    {
        public DateTime Date { get; set; }

        public string? ArrivalTime { get; set; }
        public int Emp_Id { get; set; }

<<<<<<< HEAD
        public virtual Employee? Employee { get; set; }
        public List<Employee> employees { get; set; } = new List<Employee>();
=======
        public virtual Employee? Employee { get; set; } = null!;
        public List<Employee>employees { get; set; }=new List<Employee>();
>>>>>>> d2de3bdbe40d22c357e33818d686dbde43129574
    }
}
