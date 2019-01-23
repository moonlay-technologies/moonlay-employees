using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class LeaveReadModel : ReadModel
    {
        public LeaveReadModel(Guid identity)
        {
            this.Identity = identity;
        }

        public int LeaveType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Purpose { get; set; }
        public string Delegation { get; set; }
        public double Duration { get; set; }
        public double Remaining { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public Guid EmployeeId { get; set; }
        public virtual EmployeeReadModel Employee { get; set; }

        public LeaveReadModel(Leave leave) : this(leave.Identity)
        {
            this.EmployeeId = Guid.Parse(leave.EmployeeId.ToString());
            this.LeaveType = leave.LeaveType.GetHashCode();
            this.StartDate = leave.StartDate;
            this.EndDate = leave.EndDate;
            this.Purpose = leave.Purpose;
            this.Delegation = leave.Delegation;
            this.Duration = leave.Duration;
            this.Remaining = leave.Remaining;
            this.Status = leave.Status;
            this.CreateDate = leave.CreateDate;
        }
    }
}