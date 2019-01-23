using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class DepartmentInputType : InputObjectGraphType
    {
        public DepartmentInputType()
        {
            Name = "DepartmentInputType";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<BooleanGraphType>("status");
        }
    }
}