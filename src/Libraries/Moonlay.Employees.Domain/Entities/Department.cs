using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Entities
{
    public class Department : Entity
    {

        public string Name { get; set; }
        public bool Status { get; set; }

        public Department(Guid id, string name, bool status)
        {
            Identity = id;
            Name = name;
            Status = status;

            this.AddDomainEvent(new DepartmentCreated(this));
        }
    }
}
