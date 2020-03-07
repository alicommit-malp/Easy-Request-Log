using System.Net;
using Easy_Request_log.data.entity;
using Microsoft.EntityFrameworkCore;

namespace Easy_Request_log.data
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class RequestLoggerDbContext : DbContext
    {
        public DbSet<RequestLog> RequestLogs { get; set; }

        public RequestLoggerDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestLog>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.HasIndex(i => i.Datetime);
                entity.HasIndex(i => i.Username);
                entity.HasIndex(i => i.StatusCode);
                entity.Property(p => p.StatusCode).HasConversion(
                    c => (int) c,
                    c => (HttpStatusCode) c);
            });
        }
    }
}