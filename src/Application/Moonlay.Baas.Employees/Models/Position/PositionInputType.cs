using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class PositionInputType : InputObjectGraphType
    {
        public PositionInputType()
        {
            Name = "PositionInputType";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<BooleanGraphType>("status");
        }
    }
}