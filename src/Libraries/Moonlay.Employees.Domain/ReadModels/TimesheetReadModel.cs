using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class TimesheetReadModel : ReadModel
    {
        public TimesheetReadModel(Guid identity)
        {
            this.Identity = identity;
        }
        public TimesheetReadModel(Timesheet timesheet) : this(timesheet.Identity)
        {
            Identity = timesheet.Identity;
            TeamMemberId = timesheet.TeamMemberId;
            ProjectAssignId = timesheet.ProjectAssignId;
            Task = timesheet.Task;
        }
        public Guid TeamMemberId { get; set; }
        public Guid ProjectAssignId { get; set; }
        public string Task { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public TimeSpan? Duration { get; set; }
    }
}
