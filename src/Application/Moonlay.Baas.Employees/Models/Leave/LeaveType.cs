using Employees.Domain.Entities;
using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class LeaveType : ObjectGraphType<Leave>
    {
        public LeaveType()
        {
            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());
            Field<StringGraphType>("employeeId", resolve: context => context.Source.EmployeeId.ToString());
            Field<LeavesTypeEnum>("leaveType", resolve: context => context.Source.LeaveType);
            Field<DateTimeOffsetGraphType>("startDate", resolve: context => context.Source.StartDate);
            Field<DateTimeOffsetGraphType>("endDate", resolve: context => context.Source.EndDate);

            Field<StringGraphType>("purpose", resolve: context => context.Source.Purpose.ToString());
            Field<StringGraphType>("delegation", resolve: context => context.Source.Delegation);

            //Field<FloatGraphType>("duration", resolve: context => context.Source.Duration);
            //Field<FloatGraphType>("remaining", resolve: context => context.Source.Remaining);
            Field<BooleanGraphType>("status", resolve: context => context.Source.Status);
            Field<DateTimeOffsetGraphType>("createDate", resolve: context => context.Source.CreateDate);
        }
    }
}