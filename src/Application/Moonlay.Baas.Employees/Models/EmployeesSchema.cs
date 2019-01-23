using GraphQL;
using GraphQL.Types;

namespace Moonlay.Baas.Employees.Models
{
    public class EmployeesSchema : Schema
    {
        public EmployeesSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<EmployeesQuery>();
            Mutation = resolver.Resolve<EmployeesMutation>();
        }
    }
}