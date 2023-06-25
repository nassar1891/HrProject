using System;
using System.Collections.Generic;

namespace HrProject.Models;

public partial class Attendance
{
    public DateTime Date { get; set; }

    public TimeSpan ArrivalTime { get; set; }

    public TimeSpan DepartureTime { get; set; }

    public int Employeeid { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
