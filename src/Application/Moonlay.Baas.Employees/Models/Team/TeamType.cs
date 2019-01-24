using GraphQL.Types;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Baas.Employees.Models
{
    public class TeamType : ObjectGraphType<Team>
    {
        public TeamType()
        {
            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());
            Field<StringGraphType>("name", resolve: context => context.Source.Name.ToString());
            Field<BooleanGraphType>("status", resolve: context => context.Source.Status);
        }
    }
}
