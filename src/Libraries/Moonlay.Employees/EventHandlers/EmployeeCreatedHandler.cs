using MediatR;
using Moonlay.Employees.Domain;
using Moonlay.Employees.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moonlay.Employees.EventHandlers
{
    public sealed class EmployeeCreatedHandler : INotificationHandler<EmployeeCreated>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeCreatedHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task Handle(EmployeeCreated notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}