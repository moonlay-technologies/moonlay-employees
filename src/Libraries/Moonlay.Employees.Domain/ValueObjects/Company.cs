using System;

namespace Moonlay.Employees.Domain.ValueObjects
{
    public class Company
    {
        public Guid Identity { get; }
        public string[] Names { get; }
    }
}