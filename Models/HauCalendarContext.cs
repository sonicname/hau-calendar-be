﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace server.Models;

public partial class HauCalendarContext : DbContext
{
    public HauCalendarContext()
    {
    }

    public HauCalendarContext(DbContextOptions<HauCalendarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleDayInWeek> ScheduleDayInWeeks { get; set; }

    public virtual DbSet<ScheduleTime> ScheduleTimes { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=developer;password=hellworld;database=hau-calendar");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PRIMARY");

            entity.ToTable("schedule");

            entity.HasIndex(e => e.SubjectId, "subjectID");

            entity.HasIndex(e => e.UserId, "userID");

            entity.Property(e => e.ScheduleId).HasColumnName("scheduleID");
            entity.Property(e => e.Location)
                .HasDefaultValueSql("'1'")
                .HasColumnName("location");
            entity.Property(e => e.SubjectId).HasColumnName("subjectID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Subject).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("schedule_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("schedule_ibfk_1");
        });

        modelBuilder.Entity<ScheduleDayInWeek>(entity =>
        {
            entity.HasKey(e => e.ScheduleDayInWeekId).HasName("PRIMARY");

            entity.ToTable("scheduleDayInWeek");

            entity.HasIndex(e => e.ScheduleTimeId, "scheduleTimeID");

            entity.Property(e => e.ScheduleDayInWeekId).HasColumnName("scheduleDayInWeekID");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.LessonEnded).HasColumnName("lessonEnded");
            entity.Property(e => e.LessonStarted).HasColumnName("lessonStarted");
            entity.Property(e => e.ScheduleTimeId).HasColumnName("scheduleTimeID");

            entity.HasOne(d => d.ScheduleTime).WithMany(p => p.ScheduleDayInWeeks)
                .HasForeignKey(d => d.ScheduleTimeId)
                .HasConstraintName("scheduleDayInWeek_ibfk_1");
        });

        modelBuilder.Entity<ScheduleTime>(entity =>
        {
            entity.HasKey(e => e.ScheduleTimeId).HasName("PRIMARY");

            entity.ToTable("scheduleTime");

            entity.HasIndex(e => e.ScheduleId, "scheduleID");

            entity.Property(e => e.ScheduleTimeId).HasColumnName("scheduleTimeID");
            entity.Property(e => e.DateEnded)
                .HasColumnType("date")
                .HasColumnName("dateEnded");
            entity.Property(e => e.DateStarted)
                .HasColumnType("date")
                .HasColumnName("dateStarted");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleID");

            entity.HasOne(d => d.Schedule).WithMany(p => p.ScheduleTimes)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("scheduleTime_ibfk_1");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PRIMARY");

            entity.ToTable("subject");

            entity.Property(e => e.SubjectId).HasColumnName("subjectID");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .HasColumnName("subjectName");
            entity.Property(e => e.SubjectNumCredit)
                .HasDefaultValueSql("'1'")
                .HasColumnName("subjectNumCredit");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
