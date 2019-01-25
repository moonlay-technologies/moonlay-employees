using Employees.Domain.ReadModels;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employees.Data.EntityFrameworkCore
{
    public class EntityRegistrar : IEntityRegistrar
    {
        public void RegisterEntities(ModelBuilder builder)
        {
            builder.Entity<EmployeeReadModel>(ConfigEmployeeReadModel);

            builder.Entity<TeamReadModel>(ConfigTeamReadModel);

            builder.Entity<ProjectReadModel>(ConfigProjectReadModel);
        }

        private void ConfigProjectReadModel(EntityTypeBuilder<ProjectReadModel> etb)
        {
            etb.ToTable("Projects");
            etb.HasKey(k => k.Identity);

            etb.ApplyAuditTrail();
            etb.ApplySoftDelete();
        }

        private void ConfigTeamReadModel(EntityTypeBuilder<TeamReadModel> etb)
        {
            etb.ToTable("Teams");
            etb.HasKey(k => k.Identity);

            etb.ApplyAuditTrail();
            etb.ApplySoftDelete();
        }

        private void ConfigEmployeeReadModel(EntityTypeBuilder<EmployeeReadModel> etb)
        {
            etb.ToTable("Employees");
            etb.HasKey(k => k.Identity);

            etb.ApplyAuditTrail();
            etb.ApplySoftDelete();
        }
    }
}
