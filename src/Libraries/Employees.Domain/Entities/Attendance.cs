using Core.Domain;
using System;

namespace Employees.Domain.Entities
{
    public class Attendance : EntityBase<Attendance>
    {
        public Guid EmployeeId { get; }
        public DateTimeOffset CheckInDate { get; set; }
        public AttendanceLocaltionAllowed LocationCheckIn { get; set; }
        public DateTimeOffset? CheckOutDate { get; set; }
        public TimeSpan? Duration { get; set; }

        public Attendance(Guid identity, Guid employeeId, DateTimeOffset checkInDate, AttendanceLocaltionAllowed locationCheckIn, DateTimeOffset? checkOutDate, TimeSpan? duration) : base(identity)
        {
            EmployeeId = employeeId;
            CheckInDate = checkInDate;
            LocationCheckIn = locationCheckIn;
            CheckOutDate = checkOutDate;
            Duration = duration;
        }

        protected override Attendance GetEntity()
        {
            return this;
        }
    }

    public enum AttendanceLocaltionAllowed
    {
        MoonlayHQ = 2,
        Remote = 4,
        WFH = 8,
        Onsite = 16
    }
}
