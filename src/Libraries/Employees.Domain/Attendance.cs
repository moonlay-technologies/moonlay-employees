using Core.Domain;
using System;

namespace Employees.Domain.Entities
{
    public class Attendance : EntityBase<Attendance>
    {
        public Attendance(Guid identity, Guid employeeId) : base(identity)
        {
            EmployeeId = employeeId;
        }

        public Guid EmployeeId { get; }

        protected override Attendance GetEntity()
        {
            return this;
        }
    }
}
