using MediatR;
using Moonlay.Employees.Domain.Entities;

namespace Moonlay.Employees.Domain.Events
{
    public class LeaveCreated : INotification
    {
        public LeaveCreated(Leave leave)
        {
            Data = leave;
        }

        public Leave Data { get; set; }
    }
}