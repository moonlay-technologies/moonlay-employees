using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Domain.Entities
{
    public class Department : EntityBase<Department>
    {
        public Department(Guid identity) : base(identity)
        {
        }

        protected override Department GetEntity()
        {
            return this;
        }
    }
}
