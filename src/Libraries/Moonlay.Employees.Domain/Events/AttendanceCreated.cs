using MediatR;
using Moonlay.Employees.Domain.Entities;

namespace Moonlay.Employees.Domain.Events
{
    public class AttendanceCreated : INotification
    {
        public AttendanceCreated(Attendance attendance)
        {
            Data = attendance;
        }

        public Attendance Data { get; set; }
    }
}