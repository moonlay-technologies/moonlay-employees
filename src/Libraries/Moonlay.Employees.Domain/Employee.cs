using Moonlay.Domain;
using Moonlay.Employees.Domain.ValueObjects;
using System;

namespace Moonlay.Employees.Domain
{
    public class Employee : Entity, IAggregateRoot
    {
        public Contact Person { get; }

        public Company Company { get; }

        public Employee(Guid identity, Contact contact, Company company)
        {
            Identity = identity;
            Person = contact;
            Company = company;
        }
    }
}
