using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using Moonlay.Employees.Domain.ReadModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Employees.Domain
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        #region "Employee"
        Task<IQueryable<Employee>> GetAllAsync();
        Task<Employee> GetAsync(Guid id);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> DeleteAsync(Guid id, Employee employee);
        #endregion Employee

        #region "Attendance"
        Task<IQueryable<Attendance>> GetAllAttendancesAsync();
        Task<Attendance> GetAttendanceAsync(Guid id);
        LocationsCheckInEnum GetLocationStatus(int locationsCheckIn);
        void Update(EmployeeReadModel employeeReadModel);
        #endregion Attendance

        #region "Leave"
        Task<IQueryable<Leave>> GetAllLeavesAsync();
        Task<Leave> GetLeaveAsync(Guid id);
        LeaveTypeEnum GetLeaveType(int leaveType);
        #endregion Leave

        #region "Team"
        Task<IQueryable<Team>> GetAllTeamsAsync();
        Task<Team> GetTeamAsync(Guid id);
        Task<Team> AddTeam(Team team);
        Task<Team> DeleteTeam(Guid identity, Team team);
        Task<Team> UpdateTeam(Guid identity, Team team);
        #endregion Team

        #region "Team Member"
        Task<IQueryable<TeamMember>> GetAllTeamMemberAsync();
        Task<TeamMember> GetTeamMemberAsync(Guid id);
        Task<TeamMember> AddTeamMember(TeamMember teamMember);
        Task<TeamMember> DeleteTeamMember(TeamMember teamMember);
        Task<TeamMember> UpdateTeamMember(TeamMember teamMember);
        #endregion Team Member

        #region "Timesheet"
        Task<IQueryable<Timesheet>> GetAllTimesheetAsync();
        Task<Timesheet> GetTimesheetAsync(Guid id);
        Task<Timesheet> AddTimesheet(Timesheet timesheet);
        Task<Timesheet> DeleteTimesheet(Timesheet timesheet);
        Task<Timesheet> UpdateTimesheet(Timesheet timesheet);
        Task<Timesheet> StopTimesheetAsync(Timesheet timesheet);
        #endregion Timesheet

        #region "Position"
        Task<IQueryable<Position>> GetAllPositionAsync();
        Task<Position> GetPositionAsync(Guid id);
        Task<Position> AddPosition(Position position);
        Task<Position> DeletePosition(Position position);
        Task<Position> UpdatePosition(Position position);
        #endregion

        #region Department
        Task<IQueryable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentAsync(Guid id);
        Task<Department> AddDepartment(Department department);
        Task<Department> DeleteDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        #endregion
    }
}