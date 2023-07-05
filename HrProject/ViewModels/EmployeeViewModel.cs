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

        [MinLength(11)]
        [MaxLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public string Gender { get; set; } = null!;

        public string Nationality { get; set; } = null!;

        [MinLength(14)]
        [MaxLength(14)]
        [RegularExpression("^[0-9]{14}$", ErrorMessage = "The value must be a 14-digit number.")]
        public string NationalId { get; set; }

        public double Salary { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime BirthDate { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public TimeSpan LeaveTime { get; set; }
        public string Department { get; set; }

    }
}
