using Core.Domain;
using System;

namespace Employees.Domain.Entities
{
    public class Timesheet : EntityBase<Timesheet>
    {
        public Guid TeamId { get; }
        public Guid TeamMemberId { get; }
        public Guid ProjectAssignId { get; }
        public string Task { get; }
        public DateTimeOffset StartDate { get; }
        public DateTimeOffset? EndDate { get; }
        public TimeSpan? Duration => EndDate.HasValue ? EndDate.Value.Subtract(StartDate) : default(TimeSpan?);

        public Timesheet(Guid identity, Guid teamMemberId, Guid projectAssignId, string task, DateTimeOffset startDate, DateTimeOffset? endDate = null) : base(identity)
        {
            TeamMemberId = teamMemberId;
            ProjectAssignId = projectAssignId;
            Task = task;
            StartDate = startDate;
            EndDate = endDate;
        }

        protected override Timesheet GetEntity()
        {
            return this;
        }
    }
}
