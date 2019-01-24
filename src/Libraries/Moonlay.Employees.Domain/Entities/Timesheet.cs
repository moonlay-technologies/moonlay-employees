using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.Entities
{
    public class Timesheet : Entity
    {
        public Guid TeamMemberId { get; set; }
        public Guid ProjectAssignId { get; set; }
        public string Task { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public TimeSpan? Duration { get; set; }

        public Timesheet(Guid id, Guid teamMemberId, Guid projectAssignId, string task, DateTimeOffset startDate, DateTimeOffset? endDate, TimeSpan? duration)
        {
            Identity = id;
            TeamMemberId = teamMemberId;
            ProjectAssignId = projectAssignId;
            Task = task;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            this.AddDomainEvent(new TimesheetCreated(this));
        }

    }
}
