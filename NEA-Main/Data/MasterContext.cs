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
            entity.HasKey(e => e.Id).HasName("PK_Users");

            entity.Property(e => e.ProfileImageUrl).HasColumnName("ProfileImageURL");
        });

        modelBuilder.Entity<ChatThread>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Threads");

            entity.HasIndex(e => e.GroupChatId, "IX_Threads_GroupChatId");

            entity.HasOne(d => d.GroupChat).WithMany(p => p.ChatThreads)
                .HasForeignKey(d => d.GroupChatId)
                .HasConstraintName("FK_Threads_GroupChats_GroupChatId");
        });

        modelBuilder.Entity<GroupChat>(entity =>
        {
            entity.Property(e => e.JoinId).HasColumnName("JoinID");

            entity.HasMany(d => d.UsersWithAdmins).WithMany(p => p.JoinedGroupChats)
                .UsingEntity<Dictionary<string, object>>(
                    "AccountUserGroupChat",
                    r => r.HasOne<AccountUser>().WithMany()
                        .HasForeignKey("UsersWithAdminId")
                        .HasConstraintName("FK_AccountUserGroupChat_Users_UsersWithAdminId"),
                    l => l.HasOne<GroupChat>().WithMany().HasForeignKey("JoinedGroupChatsId"),
                    j =>
                    {
                        j.HasKey("JoinedGroupChatsId", "UsersWithAdminId");
                        j.ToTable("AccountUserGroupChat");
                        j.HasIndex(new[] { "UsersWithAdminId" }, "IX_AccountUserGroupChat_UsersWithAdminId");
                    });
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasIndex(e => e.ChatThreadId, "IX_Messages_ChatThreadId");

            entity.HasIndex(e => e.SenderId, "IX_Messages_SenderId");

            entity.HasOne(d => d.ChatThread).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatThreadId)
                .HasConstraintName("FK_Messages_Threads_ChatThreadId");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK_Messages_Users_SenderId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
