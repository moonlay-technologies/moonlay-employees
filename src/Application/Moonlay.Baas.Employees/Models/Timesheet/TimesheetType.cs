using Employees.Domain.Entities;
using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class TimesheetType : ObjectGraphType<Timesheet>
    {
        public TimesheetType()
        {
            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());
            Field<StringGraphType>("teamMemberId", resolve: context => context.Source.TeamMemberId.ToString());
            Field<StringGraphType>("projectId", resolve: context => context.Source.ProjectAssignId);
            Field<StringGraphType>("task", resolve: context => context.Source.Task);
            Field<DateTimeOffsetGraphType>("startDate", resolve: context => context.Source.StartDate);
            Field<DateTimeOffsetGraphType>("endDate", resolve: context => context.Source.EndDate);
            Field<StringGraphType>("duration", resolve: context => context.Source.Duration);
        }
    }
}