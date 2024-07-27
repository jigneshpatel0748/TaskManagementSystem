using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagementSystem.Data.Base;
using TaskManagementSystem.Data.Entity;

namespace TaskManagementSystem.Data.Context
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskNotes> TaskNotes { get; set; }
        public DbSet<TaskTracker> TaskTracker { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries<ITimeStamp>();
            foreach (var entry in entries)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    var user = httpContext.User;
                    if (user == null)
                    {
                        return await base.SaveChangesAsync(cancellationToken);
                    }
                    var currentUserId = user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (!string.IsNullOrEmpty(currentUserId))
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entry.Entity.CreatedBy = Convert.ToInt32(currentUserId);
                            entry.Entity.CreatedTime = DateTime.UtcNow;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entry.Property("CreatedBy").IsModified = false;
                            entry.Property("CreatedTime").IsModified = false;
                            entry.Entity.UpdatedBy = Convert.ToInt32(currentUserId);
                            entry.Entity.UpdatedTime = DateTime.UtcNow;
                        }
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
