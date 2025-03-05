using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.Tags).IsRequired();
                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<ShortUrl>(entity =>
            {
                entity.ToTable("ShortUrl");

                entity.HasIndex(e => e.ShortUrl1, "IX_ShortUrl");

                entity.HasIndex(e => e.ShortCode, "IX_ShortUrl_1");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.LongUrl).IsRequired();
                entity.Property(e => e.ShortCode)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.ShortUrl1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ShortUrl");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
