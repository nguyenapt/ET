using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ET.Authorization.Users;
using ET.MultiTenancy;
using ET.Entities;
using System;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ET.EntityFrameworkCore
{
    public class ETDbContext : AbpZeroDbContext<Tenant, Authorization.Roles.Role, User, ETDbContext>
    {
        ///* Define a DbSet for each entity of the application */
        public virtual DbSet<Allocation> Allocations { get; set; }
        public virtual DbSet<Beneficiary> Beneficiaries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<InvoiceInfo> InvoiceInfoes { get; set; }
        public virtual DbSet<LeavePermission> LeavePermissions { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceSkill> ResourceSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SOW> SOWs { get; set; }
        public virtual DbSet<SOWRole> SOWRoles { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskCategory> TaskCategories { get; set; }
        public virtual DbSet<TimesheetEntry> TimesheetEntries { get; set; }
        public virtual DbSet<WorkingHourRule> WorkingHourRules { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<BillingRate> BillingRates { get; set; }
        public virtual DbSet<BillingType> BillingTypes { get; set; }
        public virtual DbSet<RateType> RateTypes { get; set; }
        public virtual DbSet<ResourceRole> ResourceRoles { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }
        public virtual DbSet<AllocationTimeStamp> AllocationTimeStamps { get; set; }
        public virtual DbSet<SowRoleTimeStamp> SowRoleTimeStamps { get; set; }
        public virtual DbSet<ProjectStateType> ProjectStateTypes { get; set; }
        public virtual DbSet<InternalType> InternalTypes { get; set; }
        public virtual DbSet<DeploymentInformation> DeploymentInformations { get; set; }
        public virtual DbSet<LeaveBank> LeaveBanks { get; set; }
        public virtual DbSet<AllocationStatus> AllocationStatus { get; set; }
        public virtual DbSet<AllocationType> AllocationTypes { get; set; }
        public ETDbContext(DbContextOptions<ETDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SOW>().Property(x => x.ProjectId).HasDefaultValue(default(Guid));

            modelBuilder.Entity<Client>().Property(x => x.CreationTime).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Client>().Property(x => x.IsDeleted).HasDefaultValue(false);

            modelBuilder.Entity<Project>().Property(x => x.CreationTime).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Project>().Property(x => x.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Project>().Property(p => p.UniqueCode).UseIdentityColumn();
            modelBuilder.Entity<Project>().Property(p => p.UniqueCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            modelBuilder.Entity<SOWRole>().Property(x => x.RoleName).HasDefaultValue("");

            modelBuilder.Entity<SOW>()
                .HasOne(u => u.Project)
                .WithMany(u => u.SOWs)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(a => a.ProjectType)
                .WithMany(b => b.Projects)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resource>().Property(x => x.IsKAM).HasDefaultValue(false);

            modelBuilder.Entity<DeploymentInformation>().Property(x => x.UpdateDate).HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<LeavePermission>()
                .HasOne(u => u.Resource)
                .WithMany(u => u.LeavePermissions)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);           

            base.OnModelCreating(modelBuilder);
        }
    }
}
