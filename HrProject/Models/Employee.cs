using System;
using System.Collections.Generic;

namespace HrProject.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public int Phone { get; set; }

    public string Gender { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public int NationalId { get; set; }

    public double Salary { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? BirthDate { get; set; }

    public TimeSpan ArrivalTime { get; set; }

    public TimeSpan? DepartureTime { get; set; }

    public int Departmentid { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<EmployeeHoliday> EmployeeHolidays { get; set; } = new List<EmployeeHoliday>();
}
