using Employees.Domain.Entities;
using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class TeamMemberType : ObjectGraphType<TeamMember>
    {
        public TeamMemberType()
        {
            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());
            Field<StringGraphType>("employeeId", resolve: context => context.Source.EmployeeId.ToString());
            Field<StringGraphType>("teamId", resolve: context => context.Source.TeamId.ToString());
            Field<StringGraphType>("positionId", resolve: context => context.Source.PositionId.ToString());
        }
    }
}
