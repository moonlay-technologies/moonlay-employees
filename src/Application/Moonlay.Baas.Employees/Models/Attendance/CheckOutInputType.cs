using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class CheckOutInputType : InputObjectGraphType
    {
        public CheckOutInputType()
        {
            Name = "CheckOutInputType";
            Field<NonNullGraphType<StringGraphType>>("identity");
        }
    }
}