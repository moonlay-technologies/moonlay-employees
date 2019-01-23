using MediatR;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Events
{
    public class DepartmentCreated : INotification
    {
        public DepartmentCreated(Department department)
        {
            Data = department;
        }

        public Department Data { get; set; }
    }
}
