using HrProject.Models;
using HrProject.Validation;
using System.ComponentModel.DataAnnotations;

namespace HrProject.ViewModels
{
    public class EmployeeViewModel
    {
        [MinLength(3)]
        public string FirstName { get; set; } = null!;

        [MinLength(3)] 
        public string LastName { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        [MaxLength(11)]
        [RegularExpression(@"^(010|011|012|015)[0-9]{8}$",ErrorMessage ="Enter Valid Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Gender { get; set; } = null!;

        public string Nationality { get; set; } = null!;

        [MinLength(14)]
        [MaxLength(14)]
        [RegularExpression("^[0-9]{14}$", ErrorMessage = "The value must be a 14-digit number.")]
        public string NationalId { get; set; }


        [Range(5000,50000,ErrorMessage ="Salary Must Be between 5000 and 50000")]
        public double Salary { get; set; }

        [HireDate]
        public DateTime? HireDate { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public TimeSpan? ArrivalTime { get; set; }

		[Required]
        [Attendance]
		public TimeSpan? LeaveTime { get; set; }
        public string Department { get; set; }

    }
}
    

