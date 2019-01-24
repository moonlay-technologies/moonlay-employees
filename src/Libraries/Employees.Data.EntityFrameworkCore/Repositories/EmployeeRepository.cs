using Core.Domain.Repositories;
using Employees.Domain.ReadModels;

namespace Employees.Domain.Repositories
{
    public class EmployeeRepository : AggregateRepostory<Employee, EmployeeReadModel>, IEmployeeRepository
    {
        protected override Employee Map(EmployeeReadModel readModel)
        {
            return new Employee(readModel);
        }
    }
}
