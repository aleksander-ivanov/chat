using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chat.Data.Entities;
using Chat.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IUserResolver _userResolverService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserResolver userResolverService)
            : base(options)
        {
            _userResolverService = userResolverService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoom>()
                .HasKey(bc => new { bc.UserId, bc.RoomId });

            modelBuilder.Entity<UserRoom>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserRooms)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserRoom>()
                .HasOne(bc => bc.Room)
                .WithMany(c => c.UserRooms)
                .HasForeignKey(bc => bc.RoomId);
        }

        public DbSet<ConversationRoom> Rooms { get; set; }
        public DbSet<ChatMessage> Messages { get; set; }

        public override int SaveChanges()
        {
            OnBeforeSaveChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private void OnBeforeSaveChanges()
        {
            var now = DateTime.UtcNow;
            var userId = _userResolverService.GetCurrentUserId();
            // get entries that are being Added
            var addedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is BaseEntity<long> entity)
                {
                    entity.CreatedDate = now;
                    entity.CreatedById = userId;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is BaseEntity<long> entity)
                {
                    entity.LastModifiedDate = now;
                    entity.LastModifiedById = userId;
                }
            }
        }
    }
}
