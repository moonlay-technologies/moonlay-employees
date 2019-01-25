using Core.Domain;
using System;

namespace Employees.Domain.Entities
{
    public class JobPosition : Entity
    {
        public string Name { get; }
        public bool Status { get; }

        public JobPosition(Guid id, string name, bool status)
        {
            Name = name;
            Status = status;
        }
    }
}
