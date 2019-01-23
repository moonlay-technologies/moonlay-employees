using Moonlay.Domain;
using Moonlay.Employees.Domain.Events;
using Moonlay.Employees.Domain.ReadModels;
using System;

namespace Moonlay.Employees.Domain.Entities
{
    public sealed class Attendance : Entity
    {
        public Guid EmployeeId { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public LocationsCheckInEnum LocationCheckIn { get; set; }
        public DateTimeOffset? CheckOutDate { get; set; }

        public TimeSpan? Duration { get; set; }
        public AttendanceReadModel ReadModel { get; }

        private Attendance()
        { }

        public Attendance(Guid identity, Guid employeeId, DateTimeOffset checkInDate, LocationsCheckInEnum locationCheckIn, DateTimeOffset? checkOutDate, TimeSpan? duration)
        {
            Identity = identity;
            EmployeeId = employeeId;
            CheckInDate = checkInDate;
            LocationCheckIn = locationCheckIn;
            CheckOutDate = checkOutDate;
            Duration = duration;

            this.AddDomainEvent(new AttendanceCreated(this));
        }

        public Attendance(AttendanceReadModel readModel)
        {
            this.ReadModel = readModel;

            EmployeeId = readModel.EmployeeId;
            CheckInDate = readModel.CheckInDate;
            LocationCheckIn = (LocationsCheckInEnum)readModel.LocationCheckIn;
            CheckOutDate = readModel.CheckOutDate;
            Duration = readModel.Duration;
        }
    }

    public enum LocationsCheckInEnum
    {
        MoonlayHQ = 2,
        Remote = 4,
        WFH = 8,
        Onsite = 16
    }
}