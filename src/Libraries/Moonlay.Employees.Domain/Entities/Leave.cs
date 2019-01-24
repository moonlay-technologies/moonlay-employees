using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using Moonlay.Employees.Domain.ReadModels;
using System;

namespace Moonlay.Employees.Domain.Entities
{
    public class Leave : Entity
    {
        public Guid EmployeeId { get; set; }
        public LeaveTypeEnum LeaveType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Purpose { get; set; }
        public string Delegation { get; set; }
        public double Duration { get; set; }
        public double Remaining { get; set; }
        public bool Status { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public LeaveReadModel ReadModel { get; }

        private Leave()
        {
        }

        public Leave(Guid id, Guid employeeId, LeaveTypeEnum leaveType, DateTimeOffset startDate, DateTimeOffset endDate, string purpose, string delegation, double duration, double remaining, bool status, DateTimeOffset createDate)
        {
            Identity = id;
            EmployeeId = employeeId;
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;
            Purpose = purpose;
            Delegation = delegation;
            Duration = duration;
            Remaining = remaining;
            Status = status;
            CreateDate = createDate;

            this.AddDomainEvent(new LeaveCreated(this));
        }

        public Leave(LeaveReadModel readModel)
        {
            this.ReadModel = readModel;

            EmployeeId = readModel.EmployeeId;
            LeaveType = (LeaveTypeEnum)readModel.LeaveType;
            StartDate = readModel.StartDate;
            EndDate = readModel.EndDate;
            Purpose = readModel.Purpose;
            Delegation = readModel.Delegation;
            Duration = readModel.Duration;
            Remaining = readModel.Remaining;
            Status = readModel.Status;
            CreateDate = readModel.CreateDate;
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