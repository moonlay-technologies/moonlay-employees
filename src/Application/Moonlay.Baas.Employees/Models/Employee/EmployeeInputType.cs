using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class EmployeeInputType : InputObjectGraphType
    {
        public EmployeeInputType()
        {
            Name = "EmployeeInputType";
            Field<NonNullGraphType<StringGraphType>>("personId");
            Field<NonNullGraphType<StringGraphType>>("companyId");
            Field<DateTimeOffsetGraphType>("regisDate");
            Field<DateTimeOffsetGraphType>("resignDate");
        }
    }
}