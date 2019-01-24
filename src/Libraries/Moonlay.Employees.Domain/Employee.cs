using Moonlay.Domain;
using Moonlay.Employees.Domain.Entities;
using Moonlay.Employees.Domain.Events;
using Moonlay.Employees.Domain.ReadModels;
using Moonlay.Employees.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Employees.Domain
{
    public sealed class Employee : Entity, IAggregateRoot
    {
        public PersonId PersonId { get; }
        public CompanyId CompanyId { get; }
        public DateTimeOffset RegisDate { get; }
        public DateTimeOffset? ResignDate { get; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public IReadOnlyList<Attendance> Attendances { get; private set; }
        public IReadOnlyList<Leave> Leaves { get; private set; }
        
        protected EmployeeReadModel ReadModel { get; }

        public Employee(Guid identity, PersonId personId, CompanyId companyId, DateTimeOffset regisDate, DateTimeOffset? resignDate, int day, int month, int year)
        {
            Identity = identity;
            PersonId = personId;
            CompanyId = companyId;
            RegisDate = regisDate;
            ResignDate = resignDate;
            Day = day;
            Month = month;
            Year = year;

            this.AddDomainEvent(new EmployeeCreated(this));
        }

        public Employee(EmployeeReadModel readModel)
        {
            ReadModel = readModel;

            this.Identity = readModel.Identity;
            this.Attendances = readModel.Attendances.Select(o => new Attendance(o)).ToList().AsReadOnly();
            this.Leaves = readModel.Leaves.Select(o => new Leave(o)).ToList().AsReadOnly();
            this.PersonId = new PersonId(readModel.PersonId.ToString());
            this.CompanyId = new CompanyId(readModel.CompanyId.ToString());
            this.RegisDate = readModel.RegisDate;
            this.ResignDate = readModel.ResignDate;
            this.Day = readModel.Day;
            this.Month = readModel.Month;
            this.Year = readModel.Year;
        }
        public EmployeeReadModel GetReadModel()
        {
            return ReadModel;
        }

        #region Leave
        public Leave AddLeave(LeaveTypeEnum leavesType, DateTimeOffset startDate, DateTimeOffset endDate, string purpose, string delegation, DateTimeOffset createDate)
        {
            TimeSpan span = endDate.Subtract(startDate);
            double duration = span.TotalDays + 1;

            double remaining = 12 - duration;

            //if leave not null then remaining equal to latest remaining minus duration
            if (Leaves.Count > 0)
            {
                remaining = Leaves.Select(o => o.Remaining).Max();
                remaining = remaining - duration;
            }

            //no idea
            bool status = false;

            var leave = new Leave(Guid.NewGuid(), this.Identity, leavesType, startDate, endDate, purpose, delegation, duration, remaining, status, createDate);

            if (Leaves == null)
                Leaves = new List<Leave>().AsReadOnly();

            var list = Leaves.ToList();

            list.Add(leave);

            ReadModel.Leaves.Add(new LeaveReadModel(leave));

            Leaves = list.AsReadOnly();

            return leave;
        }

        public Leave DeleteLeave(Leave leave)
        {
            if (Leaves == null)
                Leaves = new List<Leave>().AsReadOnly();

            var list = Leaves.ToList();

            list.Remove(leave);

            ReadModel.Leaves.Remove(new LeaveReadModel(leave));

            Leaves = list.AsReadOnly();

            return leave;

        }
        #endregion

        #region Attendance
        public Attendance CheckIn(DateTimeOffset checkInDate, LocationsCheckInEnum locationStatus, DateTimeOffset? checkOutDate, TimeSpan? duration)
        {
            //validation for the same CheckInDate
            if (Attendances.Count > 0)
                if (Attendances.Select(o => o.CheckInDate.Date).Contains(checkInDate.Date))
                    return null;

            //validation for checkin more than current date
            var comparison2 = DateTimeOffset.Compare(checkInDate.Date, DateTimeOffset.Now.Date);
            if (comparison2 >= 1)
                return null;

            var attendance = new Attendance(Guid.NewGuid(), this.Identity, checkInDate, locationStatus, checkOutDate, duration);

            if (Attendances == null)
                Attendances = new List<Attendance>().AsReadOnly();

            var list = Attendances.ToList();

            list.Add(attendance);

            ReadModel.Attendances.Add(new AttendanceReadModel(attendance));

            Attendances = list.AsReadOnly();

            return attendance;
        }

        public Attendance DeleteAsync(Attendance attendance)
        {
            if (Attendances == null)
                Attendances = new List<Attendance>().AsReadOnly();

            var list = Attendances.ToList();

            list.Remove(attendance);

            ReadModel.Attendances.Remove(new AttendanceReadModel(attendance));

            Attendances = list.AsReadOnly();

            return attendance;
        }

        public Attendance CheckOut(Attendance attendance)
        {
            if (Attendances == null)
                Attendances = new List<Attendance>().AsReadOnly();

            var list = Attendances.ToList();

            list.Add(attendance);

            ReadModel.Attendances.Add(new AttendanceReadModel(attendance));

            Attendances = list.AsReadOnly();

            return attendance;
        }
        #endregion

    }
}