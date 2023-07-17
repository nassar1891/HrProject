using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Models;

public class HrContext : IdentityDbContext<HrUser>
{
    public HrContext()
    {
    }

    public HrContext(DbContextOptions<HrContext> options) : base(options) { }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeHoliday> EmployeeHolidays { get; set; }
    public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }
    public virtual DbSet<Vacations> Vacations { get; set; }

}
