using Moonlay.Domain;
using System;
using System.Collections.Generic;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class EmployeeReadModel : ReadModel
    {
        public Guid CompanyId { get; set; }
        public Guid PersonId { get; set; }
        public DateTimeOffset RegisDate { get; set; }
        public DateTimeOffset? ResignDate { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public ICollection<AttendanceReadModel> Attendances { get; set; }
        public ICollection<LeaveReadModel> Leaves { get; set; }

        public EmployeeReadModel(Guid identity)
        {
            this.Identity = identity;
            Attendances = new List<AttendanceReadModel>();
            Leaves = new List<LeaveReadModel>();
        }
    }
}