using System;

namespace Moonlay.Baas.Employees.Models
{
    public class EmployeeForm
    {
        public string PersonId { get; set; }
        public string CompanyId { get; set; }
        public DateTimeOffset? regisDate { get; set; }
        public DateTimeOffset? resignDate { get; set; }
    }
}