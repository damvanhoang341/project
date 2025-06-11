using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Server.Models;

public partial class QuizDbContext : DbContext
{
    public QuizDbContext()
    {
    }

    public QuizDbContext(DbContextOptions<QuizDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Answer__3213E83FEFECF5A2");

            entity.ToTable("Answer");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Questionid)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("questionid");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.Questionid)
                .HasConstraintName("FK__Answer__question__5EBF139D");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F7D286E16");

            entity.ToTable("Question");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.Correctanswer)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("correctanswer");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3213E83F4B1E7F4B");

            entity.ToTable("Test");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");

            entity.HasMany(d => d.Questions).WithMany(p => p.Tests)
                .UsingEntity<Dictionary<string, object>>(
                    "QuestionTest",
                    r => r.HasOne<Question>().WithMany()
                        .HasForeignKey("Questionid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Question___quest__6477ECF3"),
                    l => l.HasOne<Test>().WithMany()
                        .HasForeignKey("Testid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Question___testi__6383C8BA"),
                    j =>
                    {
                        j.HasKey("Testid", "Questionid").HasName("PK__Question__14B6DDF6F2AEBDE9");
                        j.ToTable("Question_Test");
                        j.IndexerProperty<string>("Testid")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("testid");
                        j.IndexerProperty<string>("Questionid")
                            .HasMaxLength(10)
                            .IsUnicode(false)
                            .HasColumnName("questionid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
