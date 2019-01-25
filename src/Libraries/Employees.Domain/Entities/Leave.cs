using Core.Domain;
using System;

namespace Employees.Domain.Entities
{
    public class Leave : EntityBase<Leave>
    {
        public Guid EmployeeId { get; }
        public LeaveTypeEnum LeaveType { get; }
        public DateTimeOffset StartDate { get; }
        public DateTimeOffset EndDate { get; }
        public string Purpose { get; }
        public string Delegation { get; }
        public bool Status { get; }
        public DateTime CreateDate { get; }

        public Leave(Guid identity, Guid employeeId, LeaveTypeEnum leaveType, DateTimeOffset startDate, DateTimeOffset endDate, string purpose, string delegation, bool status) : base(identity)
        {
            EmployeeId = employeeId;
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;
            Purpose = purpose;
            Delegation = delegation;
            Status = status;
            CreateDate = DateTime.Now;
        }


        protected override Leave GetEntity()
        {
            throw new NotImplementedException();
        }
    }

    public enum LeaveTypeEnum
    {
        PL = 2,
        CL = 4,
        SL = 8,
        UL = 16
    }
}
