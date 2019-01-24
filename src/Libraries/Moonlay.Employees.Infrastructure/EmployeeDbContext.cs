using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moonlay.Employees.Domain.ReadModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moonlay.Employees.Infrastructure
{
    public class EmployeeDbContext : DbContext, IEmployeeDbContext
    {
        private readonly IMediator _mediator;

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
        { }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("EmployeeDbContext::ctor ->" + this.GetHashCode());
        }

        public DbSet<EmployeeReadModel> Employees => this.Set<EmployeeReadModel>();
        public DbSet<AttendanceReadModel> EmployeeAbsence => this.Set<AttendanceReadModel>();
        public DbSet<LeaveReadModel> EmployeeLeave => this.Set<LeaveReadModel>();
        public DbSet<TeamReadModel> Teams => this.Set<TeamReadModel>();
        public DbSet<TeamMemberReadModel> TeamMember => this.Set<TeamMemberReadModel>();
        public DbSet<TimesheetReadModel> Timesheet => this.Set<TimesheetReadModel>();
        public DbSet<PositionReadModel> Position => this.Set<PositionReadModel>();
        public DbSet<DepartmentReadModel> Department => this.Set<DepartmentReadModel>();


        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection.
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions.
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers)
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync();

            return true;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeReadModel>(ConfigEmployee);
            modelBuilder.Entity<AttendanceReadModel>(ConfigAttendance);
            modelBuilder.Entity<LeaveReadModel>(ConfigLeave);
            modelBuilder.Entity<TeamReadModel>(ConfigTeam);
            modelBuilder.Entity<TeamMemberReadModel>(ConfigTeamMember);
            modelBuilder.Entity<TimesheetReadModel>(ConfigTimesheet);
            modelBuilder.Entity<PositionReadModel>(ConfigPosition);
            modelBuilder.Entity<DepartmentReadModel>(ConfigDepartment);
        }

        private void ConfigDepartment(EntityTypeBuilder<DepartmentReadModel> builder)
        {
            builder.HasKey(p => p.Identity);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired();
        }
        private void ConfigPosition(EntityTypeBuilder<PositionReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.HasMany(p => p.TeamMembers).WithOne(p => p.Position).HasForeignKey(p => p.PositionId);
        }

        private void ConfigTeamMember(EntityTypeBuilder<TeamMemberReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.EmployeeId).IsRequired();
            builder.Property(p => p.TeamId).IsRequired();
            builder.Property(p => p.PositionId).IsRequired();
        }

        private void ConfigTeam(EntityTypeBuilder<TeamReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired();
        }

        private void ConfigLeave(EntityTypeBuilder<LeaveReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.EmployeeId).IsRequired();
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate).IsRequired();
            builder.Property(p => p.LeaveType).IsRequired();
            builder.Property(p => p.Purpose).IsRequired();
            builder.Property(p => p.Delegation);
            builder.Property(p => p.Status);
            builder.Property(p => p.Duration);
            builder.Property(p => p.Remaining);
            builder.Property(p => p.CreateDate);
        }


        private void ConfigEmployee(EntityTypeBuilder<EmployeeReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.PersonId).IsRequired();
            builder.Property(p => p.RegisDate).IsRequired();
            builder.Property(p => p.ResignDate);
            builder.Property(p => p.Day).IsRequired();
            builder.Property(p => p.Month).IsRequired();
            builder.Property(p => p.Year).IsRequired();

            builder.HasMany(p => p.Attendances).WithOne(p => p.Employee).HasForeignKey(p => p.EmployeeId);
            builder.HasMany(p => p.Leaves).WithOne(p => p.Employee).HasForeignKey(p => p.EmployeeId);
        }

        private void ConfigAttendance(EntityTypeBuilder<AttendanceReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.EmployeeId).IsRequired();
            builder.Property(p => p.CheckInDate).IsRequired();
            builder.Property(p => p.LocationCheckIn).IsRequired();
            builder.Property(p => p.CheckOutDate);
            builder.Property(p => p.Duration);
        }
        private void ConfigTimesheet(EntityTypeBuilder<TimesheetReadModel> builder)
        {
            builder.HasKey(p => p.Identity);

            builder.Property(p => p.TeamMemberId).IsRequired();
            builder.Property(p => p.ProjectAssignId).IsRequired();
            builder.Property(p => p.Task).IsRequired();
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate);
            builder.Property(p => p.Duration);
        }
    }
}