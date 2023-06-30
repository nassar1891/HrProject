using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrProject.Models;

[PrimaryKey(nameof(Holiday),nameof(Emp_Id))]
public class EmployeeHoliday
{
    public DateTime Holiday { get; set; }

    [ForeignKey("Employee")]
    public int Emp_Id { get; set; }
    public virtual Employee? Employee { get; set; }
}
