using Microsoft.EntityFrameworkCore;
using Moonlay.Domain;
using Moonlay.Employees.Domain.ReadModels;

namespace Moonlay.Employees.Infrastructure
{
    public interface IEmployeeDbContext : IUnitOfWork
    {
        DbSet<EmployeeReadModel> Employees { get; }
        DbSet<AttendanceReadModel> EmployeeAbsence { get; }
        DbSet<LeaveReadModel> EmployeeLeave { get; }
        DbSet<TeamReadModel> Teams { get; }
        DbSet<TeamMemberReadModel> TeamMember { get; }
        DbSet<TimesheetReadModel> Timesheet { get; }
        DbSet<PositionReadModel> Position { get; }
        DbSet<DepartmentReadModel> Department { get; }
    }
}