using Core.Domain.ReadModels;
using System;

namespace Employees.Domain.ReadModels
{
    public class EmployeeReadModel : ReadModelBase
    {
        public EmployeeReadModel(Guid identity) : base(identity)
        {
        }
    }
}
