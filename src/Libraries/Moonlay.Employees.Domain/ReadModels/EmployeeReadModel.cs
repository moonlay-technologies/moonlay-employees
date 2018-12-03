using Moonlay.Domain;
using System;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class EmployeeReadModel : ReadModel
    {
        public int Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid PersonId { get; set; }
    }
}
