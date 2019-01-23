using System;

namespace Moonlay.Baas.Employees.Models
{
    public class CheckInForm
    {
        public string EmployeeId { get; set; }
        public int LocationCheckIn { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
    }
}