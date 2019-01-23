using Microsoft.EntityFrameworkCore;
using Moonlay.Domain;
using Moonlay.Employees.Domain;
using Moonlay.Employees.Domain.Entities;
using Moonlay.Employees.Domain.ReadModels;
using Moonlay.Employees.Domain.ValueObjects;
using Moonlay.Employees.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Employees.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(EmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        #region Employee
        #region "query"
        public Task<Employee> GetAsync(Guid id)
        {
            var query = _dbContext.Employees.Include(i => i.Attendances).Include(i => i.Leaves).Where(o => o.Identity == id).Select(o => new Employee(o));

            return Task.FromResult(query.FirstOrDefault());
        }
        public async Task<IQueryable<Employee>> GetAllAsync()
        {
            await Task.Yield();

            CompanyId companyId = new CompanyId(Guid.NewGuid().ToString());
            PersonId personId = new PersonId(Guid.NewGuid().ToString());

            return _dbContext.Employees.Select(o => new Employee(o));
        }
        #endregion Employee-query

        #region "mutation"
        public Task<Employee> AddAsync(Employee employee)
        {
            var readModel = new EmployeeReadModel(employee.Identity)
            {
                PersonId = Guid.Parse(employee.PersonId.Value.ToString()),
                CompanyId = Guid.Parse(employee.CompanyId.Value.ToString()),
                RegisDate = employee.RegisDate,
                ResignDate = employee.ResignDate,
                Day = employee.Day,
                Month = employee.Month,
                Year = employee.Year
            };

            readModel.TransferDomainEvents(employee);

            _dbContext.Employees.Add(readModel);

            employee.ClearDomainEvents();

            return Task.FromResult(employee);
        }
        public async Task<Employee> DeleteAsync(Guid id, Employee employee)
        {
            var readModel = _dbContext.Employees.FirstOrDefault(o => o.Identity == employee.Identity);

            if (readModel is null)
            {
                return employee;
            }
            readModel.PersonId = Guid.Parse(employee.PersonId.Value.ToString());
            readModel.CompanyId = Guid.Parse(employee.CompanyId.Value.ToString());
            readModel.RegisDate = employee.RegisDate;
            readModel.ResignDate = employee.ResignDate;
            readModel.Day = employee.Day;
            readModel.Month = employee.Month;
            readModel.Year = employee.Year;

            employee.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Employees.Update(readModel);

            employee.ClearDomainEvents();

            return await Task.FromResult(employee);
        }
        #endregion Employee-mutation
        #endregion Employee

        #region Attendance
        #region "query"
        public async Task<IQueryable<Attendance>> GetAllAttendancesAsync()
        {
            await Task.Yield();
            return _dbContext.EmployeeAbsence.Include(i => i.Employee).Select(o => new Attendance(o.Identity, o.EmployeeId, o.CheckInDate, GetLocationStatus(o.LocationCheckIn), o.CheckOutDate, o.Duration != null ? o.Duration : TimeSpan.Zero));
        }

        public async Task<Attendance> GetAttendanceAsync(Guid id)
        {
            var query = await this.GetAllAttendancesAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }
        #endregion Attendance-query

        public LocationsCheckInEnum GetLocationStatus(int locationsCheckIn)
        {
            LocationsCheckInEnum locationStatus;

            switch (locationsCheckIn)
            {
                case 2:
                    locationStatus = LocationsCheckInEnum.MoonlayHQ;
                    break;

                case 4:
                    locationStatus = LocationsCheckInEnum.Remote;
                    break;

                case 8:
                    locationStatus = LocationsCheckInEnum.WFH;
                    break;

                case 16:
                    locationStatus = LocationsCheckInEnum.Onsite;
                    break;

                default:
                    locationStatus = LocationsCheckInEnum.MoonlayHQ;
                    break;
            }
            return locationStatus;
        }
        #endregion Attendance

        #region Leave
        public LeaveTypeEnum GetLeaveType(int leaveType)
        {
            LeaveTypeEnum leaveTypeStatus;

            switch (leaveType)
            {
                case 2:
                    leaveTypeStatus = LeaveTypeEnum.PL;
                    break;

                case 4:
                    leaveTypeStatus = LeaveTypeEnum.CL;
                    break;

                case 8:
                    leaveTypeStatus = LeaveTypeEnum.SL;
                    break;

                case 16:
                    leaveTypeStatus = LeaveTypeEnum.UL;
                    break;

                default:
                    leaveTypeStatus = LeaveTypeEnum.PL;
                    break;
            }

            return leaveTypeStatus;
        }

        public void Update(EmployeeReadModel readModel)
        {
            _dbContext.Employees.Update(readModel);
        }

        #region "query"
        public async Task<IQueryable<Leave>> GetAllLeavesAsync()
        {
            await Task.Yield();
            return _dbContext.EmployeeLeave.Include(i => i.Employee).Select(o => new Leave(o.Identity, o.EmployeeId, GetLeaveType(o.LeaveType), o.StartDate, o.EndDate, o.Purpose, o.Delegation, o.Duration, o.Remaining, o.Status, o.CreateDate));
        }

        public async Task<Leave> GetLeaveAsync(Guid id)
        {
            var query = await this.GetAllLeavesAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }
        #endregion Leave-query
        #endregion Leave

        #region "Team"
        #region "query"
        public async Task<IQueryable<Team>> GetAllTeamsAsync()
        {
            await Task.Yield();

            return _dbContext.Teams.Select(o => new Team(o.Identity, o.Name, o.Status));
        }
        public async Task<Team> GetTeamAsync(Guid id)
        {
            var query = await this.GetAllTeamsAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }
        #endregion Team-query

        #region "mutation"
        public Task<Team> AddTeam(Team team)
        {
            var readModel = new TeamReadModel(team.Identity)
            {
                Name = team.Name,
                Status = team.Status
            };

            readModel.TransferDomainEvents(team);

            _dbContext.Teams.Add(readModel);

            team.ClearDomainEvents();

            return Task.FromResult(team);
        }
        public async Task<Team> DeleteTeam(Guid identity, Team team)
        {
            var readModel = _dbContext.Teams.FirstOrDefault(o => o.Identity == team.Identity);

            if (readModel is null)
            {
                return team;
            }

            readModel.Name = team.Name;
            readModel.Status = false;

            team.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Teams.Update(readModel);

            team.ClearDomainEvents();

            return await Task.FromResult(team);
        }
        public async Task<Team> UpdateTeam(Guid identity, Team team)
        {
            var readModel = _dbContext.Teams.FirstOrDefault(o => o.Identity == team.Identity);

            if (readModel is null)
            {
                return team;
            }

            readModel.Name = team.Name;
            readModel.Status = team.Status;

            team.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Teams.Update(readModel);

            team.ClearDomainEvents();

            return await Task.FromResult(team);
        }
        #endregion Team-mutation
        #endregion Team

        #region "TeamMember"
        #region "query"
        public async Task<IQueryable<TeamMember>> GetAllTeamMemberAsync()
        {
            await Task.Yield();

            return _dbContext.TeamMember.Select(o => new TeamMember(o.Identity, o.EmployeeId, o.TeamId, o.PositionId));
        }
        public async Task<TeamMember> GetTeamMemberAsync(Guid id)
        {
            var query = await this.GetAllTeamMemberAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }
        #endregion TeamMember-query

        #region "mutation"
        public async Task<TeamMember> UpdateTeamMember(TeamMember teamMember)
        {
            var readModel = _dbContext.TeamMember.FirstOrDefault(o => o.Identity == teamMember.Identity);

            if (readModel is null)
            {
                return teamMember;
            }

            readModel.EmployeeId = teamMember.EmployeeId;
            readModel.TeamId = teamMember.TeamId;

            teamMember.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.TeamMember.Update(readModel);

            teamMember.ClearDomainEvents();

            return await Task.FromResult(teamMember);
        }
        public Task<TeamMember> AddTeamMember(TeamMember teamMember)
        {
            var readModel = new TeamMemberReadModel(teamMember.Identity)
            {
                EmployeeId = teamMember.EmployeeId,
                TeamId = teamMember.TeamId
            };

            readModel.TransferDomainEvents(teamMember);

            _dbContext.TeamMember.Add(readModel);

            teamMember.ClearDomainEvents();

            return Task.FromResult(teamMember);
        }
        public async Task<TeamMember> DeleteTeamMember(TeamMember teamMember)
        {

            var readModel = _dbContext.TeamMember.FirstOrDefault(o => o.Identity == teamMember.Identity);

            if (readModel is null)
            {
                return teamMember;
            }

            _dbContext.TeamMember.Remove(readModel);

            teamMember.ClearDomainEvents();

            return await Task.FromResult(teamMember);
        }
        #endregion TeamMember-mutation
        #endregion TeamMember

        #region "Timesheet"
        #region "query"
        public async Task<IQueryable<Timesheet>> GetAllTimesheetAsync()
        {
            await Task.Yield();

            return _dbContext.Timesheet.Select(o => new Timesheet(o.Identity, o.TeamMemberId, o.ProjectAssignId, o.Task, o.StartDate, o.EndDate, o.Duration));
        }
        public async Task<Timesheet> GetTimesheetAsync(Guid id)
        {
            var query = await this.GetAllTimesheetAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }
        #endregion Timesheet-query

        #region "mutation"
        public Task<Timesheet> AddTimesheet(Timesheet timesheet)
        {
            var readModel = new TimesheetReadModel(timesheet.Identity)
            {
                TeamMemberId = timesheet.TeamMemberId,
                ProjectAssignId = timesheet.ProjectAssignId,
                Task = timesheet.Task,
                StartDate = timesheet.StartDate,
                EndDate = timesheet.EndDate,
                Duration = timesheet.Duration
            };

            readModel.TransferDomainEvents(timesheet);

            _dbContext.Timesheet.Add(readModel);

            timesheet.ClearDomainEvents();

            return Task.FromResult(timesheet);
        }
        public async Task<Timesheet> StopTimesheetAsync(Timesheet timesheet)
        {
            var readModel = _dbContext.Timesheet.FirstOrDefault(o => o.Identity == timesheet.Identity);

            if (readModel is null)
            {
                return timesheet;
            }

            readModel.TeamMemberId = timesheet.TeamMemberId;
            readModel.ProjectAssignId = timesheet.ProjectAssignId;
            readModel.Task = timesheet.Task;
            readModel.StartDate = timesheet.StartDate;
            readModel.EndDate = timesheet.EndDate;

            timesheet.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Timesheet.Update(readModel);

            timesheet.ClearDomainEvents();

            return await Task.FromResult(timesheet);
        }
        public async Task<Timesheet> DeleteTimesheet(Timesheet timesheet)
        {
            var readModel = _dbContext.Timesheet.FirstOrDefault(o => o.Identity == timesheet.Identity);

            if (readModel is null)
            {
                return timesheet;
            }

            _dbContext.Timesheet.Remove(readModel);

            timesheet.ClearDomainEvents();

            return await Task.FromResult(timesheet);
        }
        public async Task<Timesheet> UpdateTimesheet(Timesheet timesheet)
        {
            var readModel = _dbContext.Timesheet.FirstOrDefault(o => o.Identity == timesheet.Identity);

            if (readModel is null)
            {
                return timesheet;
            }

            readModel.TeamMemberId = timesheet.TeamMemberId;
            readModel.ProjectAssignId = timesheet.ProjectAssignId;
            readModel.Task = timesheet.Task;
            readModel.StartDate = timesheet.StartDate;
            readModel.EndDate = timesheet.EndDate;
            readModel.Duration = timesheet.Duration;

            timesheet.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Timesheet.Update(readModel);

            timesheet.ClearDomainEvents();

            return await Task.FromResult(timesheet);
        }

        #endregion Timesheet-mutation
        #endregion Timesheet

        #region Position
        #region Position-query
        public async Task<IQueryable<Position>> GetAllPositionAsync()
        {
            await Task.Yield();

            return _dbContext.Position.Select(o => new Position(o.Identity, o.Name, o.Status));
        }
        public async Task<Position> GetPositionAsync(Guid id)
        {
            var query = await this.GetAllPositionAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }


        #endregion

        #region Position-mutation
        public Task<Position> AddPosition(Position position)
        {
            var readModel = new PositionReadModel(position.Identity)
            {
                Name = position.Name,
                Status = position.Status
            };

            readModel.TransferDomainEvents(position);

            _dbContext.Position.Add(readModel);

            position.ClearDomainEvents();

            return Task.FromResult(position);
        }

        public async Task<Position> DeletePosition(Position position)
        {
            var readModel = _dbContext.Position.FirstOrDefault(o => o.Identity == position.Identity);

            if(readModel is null)
            {
                return null;
            }

            _dbContext.Position.Remove(readModel);

            position.ClearDomainEvents();

            return await Task.FromResult(position);
        }

        public async Task<Position> UpdatePosition(Position position)
        {
            var readModel = _dbContext.Position.FirstOrDefault(o => o.Identity == position.Identity);

            if(readModel is null)
            {
                return null;
            }
            readModel.Name = position.Name;
            readModel.Status = position.Status;

            position.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Position.Update(readModel);

            position.ClearDomainEvents();

            return await Task.FromResult(position);
        }
        #endregion
        #endregion

        #region Department
        #region Department-query
        public async Task<IQueryable<Department>> GetAllDepartmentsAsync()
        {
            await Task.Yield();

            return _dbContext.Department.Select(o => new Department(o.Identity, o.Name, o.Status));
        }
        public async Task<Department> GetDepartmentAsync(Guid id)
        { 
            var query = await this.GetAllDepartmentsAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }
        #endregion Department-query

        #region Department-mutation
        public Task<Department> AddDepartment(Department department)
        {
            var readModel = new DepartmentReadModel(department.Identity)
            {
                Name = department.Name,
                Status = department.Status
            };

            readModel.TransferDomainEvents(department);

            _dbContext.Department.Add(readModel);

            department.ClearDomainEvents();

            return Task.FromResult(department);
        }

        public async Task<Department> DeleteDepartment(Department department)
        {
            var readModel = _dbContext.Department.FirstOrDefault(o => o.Identity == department.Identity);

            if (readModel is null)
            {
                return null;
            }

            _dbContext.Department.Remove(readModel);

            department.ClearDomainEvents();

            return await Task.FromResult(department);
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var readModel = _dbContext.Department.FirstOrDefault(o => o.Identity == department.Identity);

            if (readModel is null)
            {
                return null;
            }
            readModel.Name = department.Name;
            readModel.Status = department.Status;

            department.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Department.Update(readModel);

            department.ClearDomainEvents();

            return await Task.FromResult(department);
        }
        #endregion
        #endregion Department
    }
}