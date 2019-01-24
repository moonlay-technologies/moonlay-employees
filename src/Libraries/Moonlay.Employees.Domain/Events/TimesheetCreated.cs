using MediatR;
using Moonlay.Employees.Domain.Entities;

namespace Moonlay.Employees.Domain.Events
{
    class TimesheetCreated : INotification
    {
        public TimesheetCreated(Timesheet timesheet)
        {
            Data = timesheet;
        }

        public Timesheet Data { get; set; }
    }
}
