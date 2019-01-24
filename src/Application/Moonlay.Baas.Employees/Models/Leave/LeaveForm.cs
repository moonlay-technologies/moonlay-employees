using System;

namespace Moonlay.Baas.Employees.Models
{
    public class LeaveForm
    {
        public int LeaveType { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Purpose { get; set; }
        public string Delegation { get; set; }
        public string EmployeeId { get; set; }
        //public string Status { get; set; }
        //public double Remaining { get; set; }
        //public double Duration { get; set; }
    }
}