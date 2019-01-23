using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class TeamInputType : InputObjectGraphType
    {
        public TeamInputType()
        {
            Name = "TeamInputType";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<BooleanGraphType>>("status");
        }
    }
}