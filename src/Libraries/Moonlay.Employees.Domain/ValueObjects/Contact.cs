using System;

namespace Moonlay.Employees.Domain.ValueObjects
{
    public class Contact
    {
        public Guid Identity { get; }
        public string[] Names { get; }
    }
}