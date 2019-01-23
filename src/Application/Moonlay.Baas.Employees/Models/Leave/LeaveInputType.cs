using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class LeaveInputType : InputObjectGraphType
    {
        public LeaveInputType()
        {
            Name = "LeaveInputType";
            Field<NonNullGraphType<StringGraphType>>("employeeId");
            Field<NonNullGraphType<LeavesTypeEnum>>("leaveType");
            Field<NonNullGraphType<DateTimeOffsetGraphType>>("startDate");
            Field<NonNullGraphType<DateTimeOffsetGraphType>>("endDate");
            Field<NonNullGraphType<StringGraphType>>("purpose");
            Field<StringGraphType>("delegation"); 
        }
    }
}