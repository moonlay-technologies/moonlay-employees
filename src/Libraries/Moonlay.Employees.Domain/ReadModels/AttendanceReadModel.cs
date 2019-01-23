using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using System;

namespace Moonlay.Employees.Domain.ReadModels
{
    public class AttendanceReadModel : ReadModel
    {
        public AttendanceReadModel(Guid identity)
        {
            this.Identity = identity;
        }

        public AttendanceReadModel(Attendance attendance) : this(attendance.Identity)
        {
            this.EmployeeId = Guid.Parse(attendance.EmployeeId.ToString());
            this.CheckInDate = attendance.CheckInDate;
            this.LocationCheckIn = attendance.LocationCheckIn.GetHashCode();

            this.CheckOutDate = attendance.CheckOutDate;
            this.Duration = attendance.Duration;
        }

        public DateTimeOffset CheckInDate { get; set; }
        public int LocationCheckIn { get; set; }
        public DateTimeOffset? CheckOutDate { get; set; }
        public TimeSpan? Duration { get; set; }

        public Guid EmployeeId { get; set; }
        public virtual EmployeeReadModel Employee { get; set; }
    }
}