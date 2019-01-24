using System;

namespace Moonlay.Baas.Employees.Models
{
    public class TimesheetForm
    {
        public string TeamMemberId { get; set; }
        public string ProjectId { get; set; }
        public string Task { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
    }
}