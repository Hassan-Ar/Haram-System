using Haram.RemittanceSystem.Currencies;
using Haram.RemittanceSystem.Customers;
using Haram.RemittanceSystem.Remittances;
using Haram.RemittanceSystem.Statuses;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Haram.RemittanceSystem.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class RemittanceSystemDbContext :
    AbpDbContext<RemittanceSystemDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public RemittanceSystemDbContext(DbContextOptions<RemittanceSystemDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Remittance>(entity =>
        {
            entity.ToTable(RemittanceSystemConsts.DbTablePrefix + "Remittance", RemittanceSystemConsts.DbSchema);
            entity.HasOne(d => d.Sender)
                .WithMany(p => p.SendedRemittances)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_remittances_customers_SenderId");

            entity.HasOne(d => d.Receiver)
                .WithMany(p => p.ReceivedRemittances)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_remittances_customers_ReceiverId");
        });
        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(RemittanceSystemConsts.DbTablePrefix + "YourEntities", RemittanceSystemConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        
        builder.Entity<Currency>(b =>
        {
            b.ToTable(RemittanceSystemConsts.DbTablePrefix + "Currencies", RemittanceSystemConsts.DbSchema);
            b.HasIndex(p => p.AlphabeticCode).IsUnique();
            b.ConfigureByConvention(); 
        });
        
        builder.Entity<Status>(b =>
        {
            b.ToTable(RemittanceSystemConsts.DbTablePrefix + "Statuses", RemittanceSystemConsts.DbSchema);
            b.ConfigureByConvention(); 
        });
        
        builder.Entity<Customer>(b =>
        {
            b.ToTable(RemittanceSystemConsts.DbTablePrefix + "Customers", RemittanceSystemConsts.DbSchema);
            b.ConfigureByConvention(); 
        });
    }


    public DbSet<Customer> Customers { get; set; }
    public DbSet<Remittance> Remittances { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<Status> Statuses { get; set; }

    

}
