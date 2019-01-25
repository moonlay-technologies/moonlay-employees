using Employees.Domain.Entities;
using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class AttendanceType : ObjectGraphType<Attendance>
    {
        public AttendanceType()
        {
            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());
            Field<StringGraphType>("employeeId", resolve: context => context.Source.EmployeeId.ToString());
            Field<DateGraphType>("checkInDate", resolve: context => context.Source.CheckInDate);
            Field<LocationsCheckInEnum>("locationCheckIn", resolve: context => context.Source.LocationCheckIn);
            Field<DateGraphType>("checkOutDate", resolve: context => context.Source.CheckOutDate);
            Field<StringGraphType>("duration", resolve: context => context.Source.Duration);
        }
    }
}