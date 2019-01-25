using Employees.Domain.Commands;
using GraphQL.Types;
using MediatR;
using System;

namespace Moonlay.Baas.Employees.Models
{
    public class EmployeesMutation : ObjectGraphType
    {
        public EmployeesMutation(IMediator mediator)
        {
            #region Employee

            Field<EmployeeType>(
            "newEmployee",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EmployeeInputType>> { Name = "employee" }
                ),
                resolve: context =>
                {
                    var arg = context.GetArgument<AddEmployeeCommand>("employee");

                    return mediator.Send(arg).Result;
                });

            //Field<EmployeeType>(
            //"deleteEmployee",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteEmployeeAsync(Guid.Parse(arg), DateTimeOffset.Now));
            //    });

            #endregion Employee

            //#region Attendance
            //Field<AttendanceType>(
            //"checkIn",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<CheckInInputType>> { Name = "newcheckIn" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<CheckInForm>("newcheckIn");

            //        return newEmployeeService.CheckInAsync(Guid.Parse(arg.EmployeeId), arg.CheckInDate, arg.LocationCheckIn, arg.CheckOutDate).Result;
            //    });
            //Field<AttendanceType>(
            //"checkOut",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");

            //        return newEmployeeService.CheckOutAsync(Guid.Parse(arg), DateTimeOffset.Now);
            //    });
            //Field<AttendanceType>(
            //"deleteAttendance",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteAttendanceAsync(Guid.Parse(arg)));
            //    });

            //#endregion Attendance

            //#region Leave
            //Field<LeaveType>(
            //"newLeave",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<LeaveInputType>> { Name = "leave" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<LeaveForm>("leave");

            //        return newEmployeeService.AddLeaveAsync(Guid.Parse(arg.EmployeeId), arg.LeaveType, arg.StartDate, arg.EndDate, arg.Purpose, arg.Delegation);
            //    });
            //Field<LeaveType>(
            //"deleteLeave",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteLeaveAsync(Guid.Parse(arg)));
            //    });
            //#endregion Leave

            //#region Team
            //Field<TeamType>(
            //"newTeam",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<TeamInputType>> { Name = "team" }
            //        ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<TeamForm>("team");
            //        return newEmployeeService.AddTeamAsync(arg.Name, arg.Status);
            //    });
            //Field<TeamType>(
            //"deleteTeam",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteTeamAsync(Guid.Parse(arg)));
            //    });
            //Field<TeamType>(
            //"updateTeam",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" },
            //        new QueryArgument<NonNullGraphType<TeamInputType>> { Name = "team" }
            //    ),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("identity");
            //        var arg = context.GetArgument<TeamForm>("team");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.UpdateTeamAsync(Guid.Parse(id), arg.Name, arg.Status));
            //    });
            //#endregion

            //#region TeamMember
            //Field<TeamMemberType>(
            //"newTeamMember",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<TeamMemberInputType>> { Name = "team" }
            //        ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<TeamMemberForm>("team");
            //        return newEmployeeService.AddTeamMemberAsync(Guid.Parse(arg.employeeId), Guid.Parse(arg.teamId), Guid.Parse(arg.positionId.ToString()));
            //    });
            //Field<TeamMemberType>(
            //"deleteTeamMember",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteTeamMemberAsync(Guid.Parse(arg)));
            //    });
            //Field<TeamMemberType>(
            //"updateTeamMember",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" },
            //        new QueryArgument<NonNullGraphType<TeamMemberInputType>> { Name = "teamMember" }
            //    ),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("identity");
            //        var arg = context.GetArgument<TeamMemberForm>("teamMember");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.UpdateTeamMemberAsync(new TeamMember(Guid.Parse(id.ToString()), Guid.Parse(arg.employeeId.ToString()), Guid.Parse(arg.teamId.ToString()), Guid.Parse(arg.positionId.ToString()))));
            //    });
            //#endregion

            //#region  Timesheet
            //Field<TimesheetType>(
            //"newTimesheet",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<TimesheetInputType>> { Name = "timesheet" }
            //        ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<TimesheetForm>("timesheet");
            //        return newEmployeeService.AddTimesheetAsync(Guid.Parse(arg.TeamMemberId), Guid.Parse(arg.ProjectId), arg.Task, arg.StartDate, arg.EndDate);
            //    });
            //Field<TimesheetType>(
            //"deleteTimesheet",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteTimesheetAsync(Guid.Parse(arg)));
            //    });
            //Field<TimesheetType>(
            //"updateTimesheet",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" },
            //        new QueryArgument<NonNullGraphType<TimesheetInputType>> { Name = "timesheet" }
            //    ),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("identity");
            //        var arg = context.GetArgument<TimesheetForm>("timesheet");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.UpdateTimesheetAsync(new Timesheet(Guid.Parse(id), Guid.Parse(arg.TeamMemberId), Guid.Parse(arg.ProjectId), arg.Task, arg.StartDate, arg.EndDate, null)));
            //    });
            //Field<TimesheetType>(
            //"stopTimesheet",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("identity");

            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.StopTimesheetAsync(Guid.Parse(id), DateTimeOffset.Now));
            //    });
            //#endregion

            //#region Position
            //Field<PositionType>(
            //"newPosition",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<PositionInputType>> { Name = "position" }
            //        ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<PositionForm>("position");
            //        return newEmployeeService.AddPositionAsync(arg.Name, arg.Status);
            //    });
            //Field<PositionType>(
            //"deletePosition",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeletePositionAsync(Guid.Parse(arg)));
            //    });
            //Field<PositionType>(
            //"updatePosition",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" },
            //        new QueryArgument<NonNullGraphType<PositionInputType>> { Name = "position" }
            //    ),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("identity");
            //        var arg = context.GetArgument<PositionForm>("position");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.UpdatePositionAsync(new Position(Guid.Parse(id), arg.Name, arg.Status)));
            //    });
            //#endregion

            //#region Department
            //Field<DepartmentType>(
            //"newDepartment",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<DepartmentInputType>> { Name = "department" }
            //        ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<DepartmentForm>("department");
            //        return newEmployeeService.AddDepartmentAsync(arg.Name, arg.Status);
            //    });
            //Field<DepartmentType>(
            //"deleteDepartment",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" }
            //    ),
            //    resolve: context =>
            //    {
            //        var arg = context.GetArgument<string>("identity");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.DeleteDepartmentAsync(Guid.Parse(arg)));
            //    });
            //Field<DepartmentType>(
            //"updateDepartment",
            //    arguments: new QueryArguments(
            //        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "identity" },
            //        new QueryArgument<NonNullGraphType<DepartmentInputType>> { Name = "department" }
            //    ),
            //    resolve: context =>
            //    {
            //        var id = context.GetArgument<string>("identity");
            //        var arg = context.GetArgument<DepartmentForm>("department");
            //        return context.TryAsyncResolve(
            //             c => newEmployeeService.UpdateDepartmentAsync(new Department(Guid.Parse(id), arg.Name, arg.Status)));
            //    });
            //#endregion
        }
    }
}