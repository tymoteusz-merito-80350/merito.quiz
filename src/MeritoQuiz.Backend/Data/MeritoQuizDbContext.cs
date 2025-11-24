using MeritoQuiz.Backend.Entities;
using Microsoft.EntityFrameworkCore;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Data;

public class MeritoQuizDbContext(DbContextOptions<MeritoQuizDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(32);
            entity.Property(c => c.Icon).IsRequired().HasMaxLength(32);
            entity.HasMany(c => c.Questions)
                .WithOne(q => q.Category!)
                .HasForeignKey(q => q.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Questions");
            entity.HasKey(q => q.Id);
            entity.Property(q => q.Text).IsRequired().HasMaxLength(128);
            entity.Property(q => q.ModifiedAt).IsRequired();
            entity.HasMany(q => q.Answers)
                .WithOne(a => a.Question!)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answers");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Text).IsRequired().HasMaxLength(128);
            entity.Property(a => a.Order).IsRequired();
            entity.Property(a => a.IsCorrect).IsRequired();
        });
    }
}