using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IISprojekt3.Data.IisDbContext
{
    public partial class IisdbContext : DbContext
    {
        public IisdbContext()
        {
        }

        public IisdbContext(DbContextOptions<IisdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<AnswerType> AnswerType { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<BindAsistentAssignment> BindAsistentAssignment { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionGroup> QuestionGroup { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<TestState> TestState { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasIndex(e => e.EvaluatedBy)
                    .HasName("EvaluatedBy");

                entity.HasIndex(e => e.IdQuestion)
                    .HasName("IdQuestion");

                entity.HasIndex(e => e.IdTest)
                    .HasName("IdTest");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Bool).HasColumnType("tinyint(1)");

                entity.Property(e => e.EvaluatedBy).HasColumnType("int(11)");

                entity.Property(e => e.IdQuestion).HasColumnType("int(11)");

                entity.Property(e => e.IdTest).HasColumnType("int(11)");

                entity.Property(e => e.Number).HasColumnType("decimal(11,0)");

                entity.HasOne(d => d.EvaluatedByNavigation)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.EvaluatedBy)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ANSWER_EVALUATEDBY");

                entity.HasOne(d => d.IdQuestionNavigation)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.IdQuestion)
                    .HasConstraintName("FK_ANSWER_IDQUESTION");

                entity.HasOne(d => d.IdTestNavigation)
                    .WithMany(p => p.Answer)
                    .HasForeignKey(d => d.IdTest)
                    .HasConstraintName("FK_ANSWER_IDTEST");
            });

            modelBuilder.Entity<AnswerType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_ANSWERTYPE_NAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasIndex(e => e.Author)
                    .HasName("Author");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_ASSIGNMENT_NAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Author).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Duration).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK_ASSIGNMENT_AUTHOR");
            });

            modelBuilder.Entity<BindAsistentAssignment>(entity =>
            {
                entity.HasKey(e => new { e.IdAssignment, e.IdAsistent })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.ApprovedBy)
                    .HasName("ApprovedBy");

                entity.HasIndex(e => e.IdAsistent)
                    .HasName("IdAsistent");

                entity.HasIndex(e => e.IdAssignment)
                    .HasName("IdAssignment");

                entity.Property(e => e.IdAssignment).HasColumnType("int(11)");

                entity.Property(e => e.IdAsistent).HasColumnType("int(11)");

                entity.Property(e => e.ApprovedBy).HasColumnType("int(11)");

                entity.Property(e => e.IsApproved).HasColumnType("tinyint(1)");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.BindAsistentAssignmentApprovedByNavigation)
                    .HasForeignKey(d => d.ApprovedBy)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BINDASISTENTASSIGNMENT_APPROVEDBY");

                entity.HasOne(d => d.IdAsistentNavigation)
                    .WithMany(p => p.BindAsistentAssignmentIdAsistentNavigation)
                    .HasForeignKey(d => d.IdAsistent)
                    .HasConstraintName("FK_BINDASISTENTASSIGNMENT_IDASISTENT");

                entity.HasOne(d => d.IdAssignmentNavigation)
                    .WithMany(p => p.BindAsistentAssignment)
                    .HasForeignKey(d => d.IdAssignment)
                    .HasConstraintName("FK_BINDASISTENTASSIGNMENT_IDASSIGNMENT");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasIndex(e => e.AnswerType)
                    .HasName("AnswerType");

                entity.HasIndex(e => e.IdQuestionGroup)
                    .HasName("IdQuestionGroup");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AnswerType).HasColumnType("int(11)");

                entity.Property(e => e.IdQuestionGroup).HasColumnType("int(11)");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.AnswerTypeNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.AnswerType)
                    .HasConstraintName("FK_QUESTION_ANSWERTYPE");

                entity.HasOne(d => d.IdQuestionGroupNavigation)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.IdQuestionGroup)
                    .HasConstraintName("FK_QUESTION_IDQUESTIONGROUP");
            });

            modelBuilder.Entity<QuestionGroup>(entity =>
            {
                entity.HasIndex(e => e.IdAssingment)
                    .HasName("IdAssingment");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnType("int(11)");

                entity.Property(e => e.IdAssingment).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAssingmentNavigation)
                    .WithMany(p => p.QuestionGroup)
                    .HasForeignKey(d => d.IdAssingment)
                    .HasConstraintName("FK_QUESTIONGROUP_IDASSIGNMENT");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasIndex(e => e.Author)
                    .HasName("Author");

                entity.HasIndex(e => e.EvaluatedBy)
                    .HasName("EvaluatedBy");

                entity.HasIndex(e => e.IdAssignment)
                    .HasName("IdAssignment");

                entity.HasIndex(e => e.State)
                    .HasName("State");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Author).HasColumnType("int(11)");

                entity.Property(e => e.EvaluatedBy).HasColumnType("int(11)");

                entity.Property(e => e.IdAssignment).HasColumnType("int(11)");

                entity.Property(e => e.State).HasColumnType("int(11)");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.TestAuthorNavigation)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("FK_TEST_AUTHOR");

                entity.HasOne(d => d.EvaluatedByNavigation)
                    .WithMany(p => p.TestEvaluatedByNavigation)
                    .HasForeignKey(d => d.EvaluatedBy)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TEST_EVALUATEDBY");

                entity.HasOne(d => d.IdAssignmentNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.IdAssignment)
                    .HasConstraintName("FK_TEST_IDASSIGNMENT");

                entity.HasOne(d => d.StateNavigation)
                    .WithMany(p => p.Test)
                    .HasForeignKey(d => d.State)
                    .HasConstraintName("FK_TEST_STATE");
            });

            modelBuilder.Entity<TestState>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_TESTSTATE_NAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Author)
                    .HasName("Author");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ_USER_NAME")
                    .IsUnique();

                entity.HasIndex(e => e.UserType)
                    .HasName("UserType");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Author).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.UserType).HasColumnType("int(11)");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.InverseAuthorNavigation)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_USER_AUTHOR");

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserType)
                    .HasConstraintName("FK_USER_USERTYPE");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ_USERTYPE_NAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
