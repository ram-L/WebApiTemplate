using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.Infrastructure.Data;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiTemplate.Api.Configurations
{
    /// <summary>
    /// Provides database-related configuration for the Web API application.
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Configures the application's database context options with the specified connection string.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/> to configure.</param>
        /// <param name="connectionString">The connection string to use for the database.</param>
        public static void ConfigureAppDbContext(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        /// <summary>
        /// Applies pending database migrations to ensure the database schema is up to date.
        /// </summary>
        /// <param name="app">The application's host.</param>
        public static void ApplyMigrations(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                context.Database.Migrate();

                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ICustomLogger>();
                logger.LogMessage("An error occurred while migrating the database.", Serilog.Events.LogEventLevel.Fatal, ex);
            }
        }
    }

    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Ensures the database is created
            context.Database.EnsureCreated();

            // Check if there is any user
            if (context.Accounts.Any())
            {
                return; // DB has been seeded
            }

            SeedAdminUser(context);
        }

        private static void SeedAdminUser(AppDbContext context)
        {
            // Create an admin user
            var adminUser = new Account().Create(1, AccountType.User);
            adminUser.UserProfile.Create("admin", PasswordHelper.HashPassword(adminUser, "admin12345"), "System", "Admin", "admin@example.com");

            context.Accounts.Add(adminUser);
            context.SaveChanges();

            // At this point, adminUser.Id should be set by EF

            // Create an admin role
            var adminRole = new Role().Create(adminUser.Id, "Admin", "Administrator");
            adminRole.AddClaim(PermissionResource.User, PermissionType.Read | PermissionType.Create | PermissionType.Update | PermissionType.Delete);
            adminRole.AddClaim(PermissionResource.Product, PermissionType.Full);
            adminRole.AddClaim(PermissionResource.Order, PermissionType.Read | PermissionType.Create | PermissionType.Update);
            adminRole.AddClaim(PermissionResource.Report, PermissionType.Read);
            // Create an read only role
            var readOnlyRole = new Role().Create(adminUser.Id, "ReadOnly", "Read Only");
            readOnlyRole.SetCreateAudit(adminUser.Id);
            readOnlyRole.AddClaim(PermissionResource.User, PermissionType.Read);
            readOnlyRole.AddClaim(PermissionResource.Product, PermissionType.Read);

            context.Roles.Add(adminRole);
            context.Roles.Add(readOnlyRole);
            context.SaveChanges();

            // Assign admin role to admin user
            var adminUserRole = new AccountRole().Create(adminUser.Id, adminRole.Id);

            context.AccountRoles.Add(adminUserRole);
            context.SaveChanges();
        }
    }
}
