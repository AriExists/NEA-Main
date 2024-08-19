using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NEA_Main.Models;

namespace NEA_Main.Data;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountUser> AccountUsers { get; set; }

    public virtual DbSet<AccountUserGroupChat> AccountUserGroupChats { get; set; }

    public virtual DbSet<ChatThread> ChatThreads { get; set; }

    public virtual DbSet<GroupChat> GroupChats { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KINGPIN-TERMNAL;Initial Catalog=master;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountUser>(entity =>
        {
            entity.Property(e => e.ProfileImageUrl).HasColumnName("ProfileImageURL");
        });

        modelBuilder.Entity<AccountUserGroupChat>(entity =>
        {
            entity.HasIndex(e => e.AccountUserId, "IX_AccountUserGroupChats_AccountUserId");

            entity.HasIndex(e => e.GroupChatId, "IX_AccountUserGroupChats_GroupChatId");

            entity.HasOne(d => d.AccountUser).WithMany(p => p.AccountUserGroupChats).HasForeignKey(d => d.AccountUserId);

            entity.HasOne(d => d.GroupChat).WithMany(p => p.AccountUserGroupChats).HasForeignKey(d => d.GroupChatId);
        });

        modelBuilder.Entity<ChatThread>(entity =>
        {
            entity.HasIndex(e => e.GroupChatId, "IX_ChatThreads_GroupChatId");

            entity.HasOne(d => d.GroupChat).WithMany(p => p.ChatThreads).HasForeignKey(d => d.GroupChatId);
        });

        modelBuilder.Entity<GroupChat>(entity =>
        {
            entity.Property(e => e.JoinId).HasColumnName("JoinID");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasIndex(e => e.ChatThreadId, "IX_Messages_ChatThreadId");

            entity.HasIndex(e => e.SenderId, "IX_Messages_SenderId");

            entity.HasOne(d => d.ChatThread).WithMany(p => p.Messages).HasForeignKey(d => d.ChatThreadId);

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages).HasForeignKey(d => d.SenderId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
