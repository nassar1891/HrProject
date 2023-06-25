using System;
using System.Collections.Generic;

namespace HrProject.Models;

public partial class EmployeeHoliday
{
    public int Holiday { get; set; }

    public int Employeeid { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
