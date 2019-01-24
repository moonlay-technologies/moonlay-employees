using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Baas.Employees.Models
{
    public class TeamMemberInputType : InputObjectGraphType
    {
        public TeamMemberInputType()
        {
            Name = "TeamMemberInputType";
            Field<NonNullGraphType<StringGraphType>>("employeeId");
            Field<NonNullGraphType<StringGraphType>>("teamId");
            Field<NonNullGraphType<StringGraphType>>("positionId");
        }
    }
}
