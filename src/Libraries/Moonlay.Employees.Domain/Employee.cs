using Moonlay.Domain;
using Moonlay.Employees.Domain.ValueObjects;
using System;

namespace Moonlay.Employees.Domain
{
    public class Employee : Entity, IAggregateRoot
    {
        public Guid Identity { get; }

        public Contact Contact { get; }

        public Company Company { get; }

        public Employee(Guid identity, Contact contact, Company company)
        {
            Identity = identity;
            Contact = contact;
            Company = company;
        }
    }
}
