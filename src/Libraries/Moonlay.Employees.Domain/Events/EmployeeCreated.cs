using MediatR;

namespace Moonlay.Employees.Domain.Events
{
    public class EmployeeCreated : INotification
    {
        public EmployeeCreated(Employee employee)
        {
            Data = employee;
        }

        public Employee Data { get; set; }
    }
}