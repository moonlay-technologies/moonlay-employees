using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class DepartmentReadModel : ReadModel
    {
        public DepartmentReadModel(Guid identity)
        {
            Identity = identity;
        }

        public string Name { get; set; }
        public bool Status { get; set; }

        public DepartmentReadModel(Department department) : this(department.Identity)
        {
            this.Name = department.Name;
            this.Status = department.Status;
        }
    }
}
