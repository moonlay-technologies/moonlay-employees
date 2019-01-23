using GraphQL.Types;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
