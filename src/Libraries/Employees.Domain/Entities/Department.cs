using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Entities
{
    public class Department : EntityBase<Department>
    {
        public string Name { get; set; }
        public bool Status { get; set; }

        public Department(Guid identity, string name, bool status) : base(identity)
        {
            Name = name;
            Status = status;
        }

        protected override Department GetEntity()
        {
            return this;
        }
    }
}
