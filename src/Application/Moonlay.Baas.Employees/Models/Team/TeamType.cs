using Employees.Domain;
using GraphQL.Types;

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
