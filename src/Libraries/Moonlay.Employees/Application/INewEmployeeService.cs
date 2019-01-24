using Moonlay.Employees.Domain;
using Moonlay.Employees.Domain.Entities;
using Moonlay.Employees.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Moonlay.Employees.Application
{
    public interface INewEmployeeService
    {
        #region "Employee"
        Task<Employee> AddEmployeeAsync(PersonId personId, CompanyId company, DateTimeOffset? regisDate, DateTimeOffset? resignDate);
        Task<Employee> DeleteEmployeeAsync(Guid identity, DateTimeOffset resignDate);
        #endregion Employee

        #region "Attendance"
        Task<Attendance> CheckInAsync(Guid id, DateTimeOffset? checkInDate, int locationCheckIn, DateTimeOffset? checkOutDate);
        Task<Attendance> CheckOutAsync(Guid id, DateTimeOffset checkOutDate);
        Task<Attendance> DeleteAttendanceAsync(Guid identity);
        #endregion Attendance

        #region "Leave"
        Task<Leave> AddLeaveAsync(Guid employeeId, int leaveType, DateTimeOffset startDate, DateTimeOffset endDate, string purpose, string delegation);
        Task<Leave> DeleteLeaveAsync(Guid identity);
        #endregion Leave

        #region "Team"
        Task<Team> AddTeamAsync(string name, bool? status);
        Task<Team> DeleteTeamAsync(Guid identity);
        Task<Team> UpdateTeamAsync(Guid identity, string name, bool? status);
        #endregion Team

        #region Team Member
        Task<TeamMember> AddTeamMemberAsync(Guid employeeId, Guid teamId, Guid positionId);
        Task<TeamMember> DeleteTeamMemberAsync(Guid identity);
        Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember);
        #endregion Team Member

        #region Timesheet
        Task<Timesheet> AddTimesheetAsync(Guid teamMemberId, Guid projectId, string task, DateTimeOffset? startDate, DateTimeOffset? endDate);
        Task<Timesheet> UpdateTimesheetAsync(Timesheet timesheet);
        Task<Timesheet> DeleteTimesheetAsync(Guid identity);
        Task<Timesheet> StopTimesheetAsync(Guid id, DateTimeOffset endDate);
        #endregion Timesheet

        #region Position
        Task<Position> AddPositionAsync(string name, bool status);
        Task<Position> DeletePositionAsync(Guid identity);
        Task<Position> UpdatePositionAsync(Position position);
        #endregion

        #region Department
        Task<Department> AddDepartmentAsync(string name, bool status);
        Task<Department> DeleteDepartmentAsync(Guid identity);
        Task<Department> UpdateDepartmentAsync(Department department);
        #endregion
    }
}