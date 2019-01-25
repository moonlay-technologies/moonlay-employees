using Core.Domain;
using Employees.Domain.ReadModels;
using System;

namespace Employees.Domain
{
    public class Employee : AggregateRoot<Employee, EmployeeReadModel>
    {
        public Guid PersonId { get; }
        public Guid CompanyId { get; }
        public DateTimeOffset RegisDate { get; }
        public DateTimeOffset? ResignDate { get; }

        public Employee(Guid identity, Guid personId, Guid companyId, DateTimeOffset regisDate) : base(identity)
        {
            PersonId = personId;
            CompanyId = companyId;

            PersonId = personId;
            CompanyId = companyId;
            RegisDate = regisDate;
            //Day = day;
            //Month = month;
            //Year = year;
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
