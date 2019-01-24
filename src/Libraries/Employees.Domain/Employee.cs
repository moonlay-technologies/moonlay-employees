using Core.Domain;
using Employees.Domain.ReadModels;
using System;

namespace Employees.Domain
{
    public class Employee : AggregateRoot<Employee, EmployeeReadModel>
    {
        public Guid PersonId { get; }
        public Guid CompanyId { get; }

        public Employee(Guid identity, Guid personId, Guid companyId) : base(identity)
        {
            PersonId = personId;
            CompanyId = companyId;
        }

        public Employee(EmployeeReadModel readModel) : base(readModel)
        {
        }

        protected override Employee GetEntity()
        {
            return this;
        }
    }
}
