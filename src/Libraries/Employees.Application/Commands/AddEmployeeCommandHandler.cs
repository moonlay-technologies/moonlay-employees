using Core.Domain.Commands;
using Employees.Domain.Repositories;
using ExtCore.Data.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Employees.Domain.Commands
{
    public class AddEmployeeCommandHandler : ICommandHandler<AddEmployeeCommand, Employee>
    {
        private readonly IStorage _storage;
        private readonly IEmployeeRepository _repoEmployee;

        public AddEmployeeCommandHandler(IStorage storage)
        {
            _storage = storage;
            _repoEmployee = _storage.GetRepository<IEmployeeRepository>();
        }

        public async Task<Employee> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var record = new Employee(Guid.NewGuid(), request.PersonId, request.CompanyId, DateTime.Now);

            await _repoEmployee.Update(record);

            return record;
        }
    }
}
