using Core.Domain.Repositories;
using Employees.Domain.ReadModels;

namespace Employees.Domain.Repositories
{
    public interface IEmployeeRepository : IAggregateRepository<Employee, EmployeeReadModel>
    {
    }
}
