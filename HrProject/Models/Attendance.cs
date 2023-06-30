using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrProject.Models;



[PrimaryKey(nameof(Date),nameof(Emp_Id))]
public class Attendance
{
    public DateTime Date { get; set; }

    public TimeSpan ArrivalTime { get; set; }

    public TimeSpan DepartureTime { get; set; }

    [ForeignKey("Employee")]
    public int? Emp_Id { get; set; }

    public virtual Employee? Employee { get; set; } = null!;
}
