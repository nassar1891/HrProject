using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
	public class AttendanceViewModel
	{
		[Required(ErrorMessage ="Please Choose Date")]
		public DateTime Date { get; set; }

		[Required(ErrorMessage ="Please Signin")]
		public DateTime ArrivalTime { get; set; }

		[Required(ErrorMessage ="Please Chechout")]
		public DateTime DepartureTime { get; set; }
        public int? EmpId { get; set; }

        //-------------------

        public int? Bounshour { get; set; }
        public int? DiscountHour { get; set; }
    }
}
