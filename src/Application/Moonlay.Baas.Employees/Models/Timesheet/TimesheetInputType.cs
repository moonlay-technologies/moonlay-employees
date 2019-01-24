using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class TimesheetInputType : InputObjectGraphType
    {
        public TimesheetInputType()
        {
            Name = "TimesheetInputType";
            Field<NonNullGraphType<StringGraphType>>("teamMemberId");
            Field<NonNullGraphType<StringGraphType>>("projectId");
            Field<NonNullGraphType<StringGraphType>>("task");
            Field<NonNullGraphType<DateTimeOffsetGraphType>>("startDate");
            Field<DateTimeOffsetGraphType>("endDate");
        }
    }
}