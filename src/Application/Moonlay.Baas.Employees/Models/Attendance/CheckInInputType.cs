using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class CheckInInputType : InputObjectGraphType
    {
        public CheckInInputType()
        {
            Name = "CheckInInputType";
            Field<NonNullGraphType<StringGraphType>>("employeeId");
            Field<NonNullGraphType<LocationsCheckInEnum>>("locationCheckIn");
            Field<DateTimeGraphType>("checkInDate");
            Field<DateTimeGraphType>("checkOutDate");
        }
    }
}