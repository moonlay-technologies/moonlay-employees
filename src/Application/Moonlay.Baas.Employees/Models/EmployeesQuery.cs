using GraphQL.Types;
using Moonlay.Employees.Domain;
using System;
using System.Linq;

namespace Moonlay.Baas.Employees.Models
{
    public class EmployeesQuery : ObjectGraphType
    {
        public EmployeesQuery(IEmployeeRepository repo)
        {
            #region Employee
            Description = "Get Employee by Id";
            Field<EmployeeType>(
                "employee",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var employee = repo.GetAsync(Guid.Parse(arg)).Result;
                    return employee;
                }
            );

            Description = "Get All Employees";
            Field<ListGraphType<EmployeeType>>(
                "employees",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");

                    var query = repo.GetAllAsync().Result;

                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
            );

            #endregion Employee

            #region Attendance

            Field<AttendanceType>(
                "attendance",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var attendance = repo.GetAttendanceAsync(Guid.Parse(arg)).Result;
                    return attendance;
                }
            );

            Field<ListGraphType<AttendanceType>>(
                "attendances",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");

                    var query = repo.GetAllAttendancesAsync().Result;

                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
            );

            #endregion Attendance

            #region Leave
            Field<ListGraphType<LeaveType>>(
                "leaves",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");
                    var query = repo.GetAllLeavesAsync().Result;
                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
           );
            Field<LeaveType>(
                "leave",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var leave = repo.GetLeaveAsync(Guid.Parse(arg)).Result;
                    return leave;
                }
            );
            #endregion Leave

            #region "Team"
            Field<ListGraphType<TeamType>>(
                "teams",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");
                    var query = repo.GetAllTeamsAsync().Result;
                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
           );
            Field<TeamType>(
                "team",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var team = repo.GetTeamAsync(Guid.Parse(arg)).Result;
                    return team;
                }
            );
            #endregion

            #region "Team Member"
            Field<ListGraphType<TeamMemberType>>(
                "teamMembers",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");
                    var query = repo.GetAllTeamMemberAsync().Result;
                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
           );
            Field<TeamMemberType>(
                "teamMember",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var team = repo.GetTeamMemberAsync(Guid.Parse(arg)).Result;
                    return team;
                }
            );
            #endregion

            #region "Timesheet"

            Field<ListGraphType<TimesheetType>>(
                "timesheets",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");
                    var query = repo.GetAllTimesheetAsync().Result;
                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
           );
            Field<TimesheetType>(
                "timesheet",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var timesheet = repo.GetTimesheetAsync(Guid.Parse(arg)).Result;
                    return timesheet;
                }
            );
            #endregion

            #region Position
            Field<ListGraphType<PositionType>>(
                "positions",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),

                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");
                    var query = repo.GetAllPositionAsync().Result;
                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
            );
            Field<PositionType>(
                "position",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),
                
                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var position = repo.GetPositionAsync(Guid.Parse(arg)).Result;
                    return position;
                }
            );
            #endregion

            #region Department
            Field<ListGraphType<DepartmentType>>(
                "departments",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),

                resolve: context =>
                {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");
                    var query = repo.GetAllDepartmentsAsync().Result;
                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
            );
            Field<DepartmentType>(
                "department",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }),

                resolve: context =>
                {
                    var arg = context.GetArgument<string>("identity");
                    var department = repo.GetDepartmentAsync(Guid.Parse(arg)).Result;
                    return department;
                }
            );
            #endregion
        }
    }
}