using System;
using AttendeeTickerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AttendeeTickerAPI.DAL
{
    public partial class AttendeeTickerDbContext : DbContext
    {
        public AttendeeTickerDbContext()
        {
        }

        public AttendeeTickerDbContext(DbContextOptions<AttendeeTickerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<AttendanceDetails> AttendanceDetails { get; set; }
        public virtual DbSet<ComputerFile> ComputerFile { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Shift> Shift { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectClass> SubjectClass { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=W0109162866\\SQLEXPRESS;Database=AttendeeTickerDb;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.AttendanceID).HasColumnName("AttendanceID");

                entity.Property(e => e.StudentID)
                    .HasColumnName("StudentID")
                    .HasMaxLength(300);

                entity.Property(e => e.SubjectClassID).HasColumnName("SubjectClassID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_Attendance_Student");

                entity.HasOne(d => d.SubjectClass)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.SubjectClassID)
                    .HasConstraintName("FK_Attendance_SubjectClass");
            });

            modelBuilder.Entity<AttendanceDetails>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.AttendanceID)
                    .HasColumnName("AttendanceID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EventID).HasColumnName("EventID");

                entity.HasOne(d => d.Attendance)
                    .WithMany()
                    .HasForeignKey(d => d.AttendanceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttendanceDetails_Attendance");

                entity.HasOne(d => d.Event)
                    .WithMany()
                    .HasForeignKey(d => d.EventID)
                    .HasConstraintName("FK_AttendanceDetails_Event");
            });

            modelBuilder.Entity<ComputerFile>(entity =>
            {
                entity.HasKey(e => e.FileID)
                    .HasName("PK_File");

                entity.Property(e => e.FileID).HasColumnName("FileID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.FileType).HasMaxLength(10);

                entity.Property(e => e.StudentID)
                    .HasColumnName("StudentID")
                    .HasMaxLength(300);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ComputerFile)
                    .HasForeignKey(d => d.StudentID)
                    .HasConstraintName("FK_ComputerFile_Student");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventID).HasColumnName("EventID");

                entity.Property(e => e.DateTime).HasColumnType("date");

                entity.Property(e => e.ShiftID).HasColumnName("ShiftID");

                entity.Property(e => e.SubjectClassID).HasColumnName("SubjectClassID");

                entity.Property(e => e.TeacherID).HasColumnName("TeacherID");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.ShiftID)
                    .HasConstraintName("FK_Event_Shift");

                entity.HasOne(d => d.SubjectClass)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.SubjectClassID)
                    .HasConstraintName("FK_Event_SubjectClass");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.TeacherID)
                    .HasConstraintName("FK_Event_Teacher");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.ShiftID).HasColumnName("ShiftID");

                entity.Property(e => e.ShiftName).HasMaxLength(50);

                entity.Property(e => e.ShiftStart).HasColumnType("time(0)");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentID)
                    .HasColumnName("StudentID")
                    .HasMaxLength(300);

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Nationality).HasMaxLength(20);

                entity.Property(e => e.PersonID).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.StudentFirstName).HasMaxLength(20);

                entity.Property(e => e.StudentLastName).HasMaxLength(20);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectID).HasColumnName("SubjectID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.SubjectName).HasMaxLength(50);

                entity.Property(e => e.UpdatedBy).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("date");
            });

            modelBuilder.Entity<SubjectClass>(entity =>
            {
                entity.Property(e => e.SubjectClassID).HasColumnName("SubjectClassID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.SubjectClassName).HasMaxLength(50);

                entity.Property(e => e.SubjectID).HasColumnName("SubjectID");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectClass)
                    .HasForeignKey(d => d.SubjectID)
                    .HasConstraintName("FK_SubjectClass_Subject");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.TeacherID).HasColumnName("TeacherID");

                entity.Property(e => e.TeacherName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
