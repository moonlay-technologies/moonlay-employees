using Moonlay.Employees.Domain;
using Moonlay.Employees.Domain.Entities;
using Moonlay.Employees.Domain.ValueObjects;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Moonlay.Employees.Application
{
    public class NewEmployeeService : INewEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public NewEmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #region "Employee"
        //add new employee
        public async Task<Employee> AddEmployeeAsync(PersonId personId, CompanyId companyId, DateTimeOffset? regisDate, DateTimeOffset? resignDate)
        {

            DateTimeOffset regDate = regisDate ?? DateTimeOffset.Now;
            DateTimeOffset resDate = resignDate ?? DateTimeOffset.Now;

            int year = 0;
            int month = 0;
            int day = 0;

            if (resignDate != null)
            {
                TimeSpan selisih = resDate.Subtract(regDate);

                year = Convert.ToInt32(selisih.Days / 365);
                month = Convert.ToInt32(selisih.Days % 365 / 30);
                day = selisih.Days - year * 365 - month * 30;
            }
            var employee = await _employeeRepository.AddAsync(new Employee(Guid.NewGuid(), personId, companyId, regDate, resignDate, day, month, year));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return employee;
        }
        //delete employee means update employee status to resign
        public async Task<Employee> DeleteEmployeeAsync(Guid identity, DateTimeOffset resignDate)
        {
            var employee = await _employeeRepository.GetAsync(identity);

            if (employee == null)
                return employee;

            DateTimeOffset joinDate = employee.RegisDate;
            DateTimeOffset lastDay = resignDate;
            TimeSpan selisih = lastDay.Subtract(joinDate);

            int year = Convert.ToInt32(selisih.Days / 365);
            int month = Convert.ToInt32(selisih.Days % 365 / 30);
            int day = selisih.Days - year * 365 - month * 30;

            var updatedEmployee = await _employeeRepository.DeleteAsync(identity, new Employee(identity, employee.PersonId, employee.CompanyId, employee.RegisDate, resignDate, day, month, year));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return updatedEmployee;
        }
        #endregion Employee

        #region "Attendance"
        public async Task<Attendance> CheckInAsync(Guid employeeId, DateTimeOffset? checkInDate, int locationCheckIn, DateTimeOffset? checkOutDate)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);

            if (employee == null)
                return null;

            var locationStatus = _employeeRepository.GetLocationStatus(locationCheckIn);


            DateTimeOffset checkIn = checkInDate ?? DateTimeOffset.Now;
            DateTimeOffset checkOut = checkOutDate ?? DateTimeOffset.Now;
            TimeSpan duration = TimeSpan.Zero;

            if (checkOutDate != null)
            {
                checkIn = DateTimeOffset.ParseExact(checkIn.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
                checkOut = DateTimeOffset.ParseExact(checkOut.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
                duration = checkOut.Subtract(checkIn);
            }

            var attendance = employee.CheckIn(checkInDate ?? DateTimeOffset.Now, locationStatus, checkOutDate ?? null, duration != null ? duration : TimeSpan.Zero);

            _employeeRepository.Update(employee.GetReadModel());

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return attendance;
        }

        public async Task<Attendance> CheckOutAsync(Guid id, DateTimeOffset checkOutDate)
        {
            var attendance = await _employeeRepository.GetAttendanceAsync(id);

            if (attendance == null)
                return null;

            //only 1 checkin with 1 checkout per day
            if (attendance.CheckOutDate != null)
                return null;

            //get duration between checkin date dan checkout date
            DateTimeOffset checkIn = DateTimeOffset.ParseExact(attendance.CheckInDate.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
            DateTimeOffset checkOut = DateTimeOffset.ParseExact(checkOutDate.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
            TimeSpan duration = checkOut.Subtract(checkIn);

            var employee = await _employeeRepository.GetAsync(attendance.EmployeeId);

            var oldAttendance = employee.DeleteAsync(new Attendance(attendance.Identity, attendance.EmployeeId, attendance.CheckInDate, attendance.LocationCheckIn, checkOut, duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            var employeeAttendance = employee.CheckOut(new Attendance(attendance.Identity, attendance.EmployeeId, attendance.CheckInDate, attendance.LocationCheckIn, checkOut, duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return employeeAttendance;
        }
        public async Task<Attendance> DeleteAttendanceAsync(Guid identity)
        {
            var attendance = await _employeeRepository.GetAttendanceAsync(identity);

            if (attendance == null)
                return attendance;

            var employee = await _employeeRepository.GetAsync(attendance.EmployeeId);

            var deletedAttendance = employee.DeleteAsync(new Attendance(attendance.Identity, attendance.EmployeeId, attendance.CheckInDate, attendance.LocationCheckIn, attendance.CheckOutDate, attendance.Duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return attendance;
        }
        #endregion Attendance

        #region "Leave"
        public async Task<Leave> AddLeaveAsync(Guid employeeId, int leaveType, DateTimeOffset startDate, DateTimeOffset endDate, string purpose, string delegation)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);

            if (employee == null)
                return null;
            var leavesType = _employeeRepository.GetLeaveType(leaveType);

            var leave = employee.AddLeave(leavesType, startDate, endDate, purpose, delegation, DateTimeOffset.Now);

            _employeeRepository.Update(employee.GetReadModel());

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return leave;
        }
        public async Task<Leave> DeleteLeaveAsync(Guid identity)
        {
            var leave = await _employeeRepository.GetLeaveAsync(identity);

            if (leave == null)
                return leave;

            var employee = await _employeeRepository.GetAsync(leave.EmployeeId);

            var deletedLeave = employee.DeleteLeave(new Leave(leave.Identity, leave.EmployeeId, leave.LeaveType, leave.StartDate, leave.EndDate, leave.Purpose, leave.Delegation, leave.Duration, leave.Remaining, leave.Status, leave.CreateDate));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return leave;
        }
        #endregion

        #region "Team"
        public async Task<Team> AddTeamAsync(string name, bool? status)
        {
            var team = await _employeeRepository.AddTeam(new Team(Guid.NewGuid(), name, status ?? true));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return team;
        }
        public async Task<Team> DeleteTeamAsync(Guid identity)
        {
            var team = await _employeeRepository.GetTeamAsync(identity);

            if (team == null)
                return team;

            var deletedTeam = await _employeeRepository.DeleteTeam(identity, new Team(identity, team.Name, team.Status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return deletedTeam;

        }
        public async Task<Team> UpdateTeamAsync(Guid identity, string name, bool? status)
        {
            //check whether selected team does exist or not
            var team = await _employeeRepository.GetTeamAsync(identity);
            if (team == null)
                return team;

            var updatedTeam = await _employeeRepository.UpdateTeam(identity, new Team(identity, name, status ?? true));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return updatedTeam;

        }
        #endregion

        #region "Team Member"
        public async Task<TeamMember> AddTeamMemberAsync(Guid employeeId, Guid teamId, Guid positionId)
        {

            var teamMember = await _employeeRepository.AddTeamMember(new TeamMember(Guid.NewGuid(), employeeId, teamId, positionId));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return teamMember;
        }
        public async Task<TeamMember> DeleteTeamMemberAsync(Guid identity)
        {
            var teamMember = await _employeeRepository.GetTeamMemberAsync(identity);

            if (teamMember == null)
                return teamMember;

            var deletedTeamMember = await _employeeRepository.DeleteTeamMember(new TeamMember(identity, teamMember.EmployeeId, teamMember.TeamId, teamMember.PositionId));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return deletedTeamMember;
        }
        public async Task<TeamMember> UpdateTeamMemberAsync(TeamMember teamMember)
        {
            var isTeamMember = await _employeeRepository.GetTeamMemberAsync(teamMember.Identity);

            if (isTeamMember == null)
                return teamMember;

            var updatedTeamMember = await _employeeRepository.UpdateTeamMember(new TeamMember(teamMember.Identity, teamMember.EmployeeId, teamMember.TeamId, teamMember.PositionId));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return updatedTeamMember;
        }
        #endregion

        #region "Timesheet"
        public async Task<Timesheet> AddTimesheetAsync(Guid teamMemberId, Guid projectId, string task, DateTimeOffset? startDate, DateTimeOffset? endDate)
        {

            DateTimeOffset start = startDate ?? DateTimeOffset.Now;
            DateTimeOffset stop = endDate ?? DateTimeOffset.Now;
            TimeSpan duration = TimeSpan.Zero;

            if (endDate != null)
            {
                start = DateTimeOffset.ParseExact(start.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
                stop = DateTimeOffset.ParseExact(stop.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
                duration = stop.Subtract(start);
            }

            var timesheet = await _employeeRepository.AddTimesheet(new Timesheet(Guid.NewGuid(), teamMemberId, projectId, task, startDate ?? DateTimeOffset.Now, endDate, duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return timesheet;
        }
        public async Task<Timesheet> DeleteTimesheetAsync(Guid identity)
        {
            var timesheet = await _employeeRepository.GetTimesheetAsync(identity);

            if (timesheet == null)
                return timesheet;

            var deleteTimesheet = await _employeeRepository.DeleteTimesheet(new Timesheet(identity, timesheet.TeamMemberId, timesheet.ProjectAssignId, timesheet.Task, timesheet.StartDate, timesheet.EndDate, timesheet.Duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return deleteTimesheet;
        }
        public async Task<Timesheet> UpdateTimesheetAsync(Timesheet timesheet)
        {
            var isTimesheet = await _employeeRepository.GetTimesheetAsync(timesheet.Identity);

            if (isTimesheet == null)
                return timesheet;

            var updatedTimesheet = await _employeeRepository.UpdateTimesheet(new Timesheet(timesheet.Identity, timesheet.TeamMemberId, timesheet.ProjectAssignId, timesheet.Task, timesheet.StartDate, timesheet.EndDate, timesheet.Duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return updatedTimesheet;
        }
        public async Task<Timesheet> StopTimesheetAsync(Guid id, DateTimeOffset endDate)
        {
            var timesheet = await _employeeRepository.GetTimesheetAsync(id);

            if (timesheet == null)
                return null;

            DateTimeOffset start = DateTimeOffset.ParseExact(timesheet.StartDate.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
            DateTimeOffset stop = DateTimeOffset.ParseExact(endDate.ToString("HH:mm:ss"), "HH:mm:ss", new DateTimeFormatInfo());
            TimeSpan duration = stop.Subtract(start);

            var updatedTimesheet = await _employeeRepository.UpdateTimesheet(new Timesheet(timesheet.Identity, timesheet.TeamMemberId, timesheet.ProjectAssignId, timesheet.Task, timesheet.StartDate, endDate, duration));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return updatedTimesheet;
        }

        #endregion Timesheet

        #region Position
        public async Task<Position> AddPositionAsync(string name, bool status)
        {
            var position = await _employeeRepository.AddPosition(new Position(Guid.NewGuid(), name, status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return position;
        }

        public async Task<Position> DeletePositionAsync(Guid identity)
        {
            var position = await _employeeRepository.GetPositionAsync(identity);

            if(position is null)
            {
                return null;
            }

            var deletePosition = await _employeeRepository.DeletePosition(new Position(identity, position.Name, position.Status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return deletePosition;
        }

        public async Task<Position> UpdatePositionAsync(Position position)
        {
            var isExist = await _employeeRepository.GetPositionAsync(position.Identity);

            if (isExist == null)
                return position;

            var updatedPosition = await _employeeRepository.UpdatePosition(new Position(position.Identity, position.Name, position.Status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return updatedPosition;
        }

        public async Task<Department> AddDepartmentAsync(string name, bool status)
        {
            var department = await _employeeRepository.AddDepartment(new Department(Guid.NewGuid(), name, status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return department;
        }

        public async Task<Department> DeleteDepartmentAsync(Guid identity)
        {
            var department = await _employeeRepository.GetDepartmentAsync(identity);

            if (department is null)
                return null;

            var deleteDepartment = await _employeeRepository.DeleteDepartment(new Department(identity, department.Name, department.Status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return deleteDepartment;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            var isExist = await _employeeRepository.GetDepartmentAsync(department.Identity);

            if (isExist is null)
                return null;

            var updatedDepartment = await _employeeRepository.UpdateDepartment(new Department(department.Identity, department.Name, department.Status));

            await _employeeRepository.UnitOfWork.SaveChangesAsync();

            return department;
        }
        #endregion
    }
}