using System;
using System.Collections.Generic;

namespace NEA_Main.Models;

public partial class GroupChat
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int JoinId { get; set; }

    public virtual ICollection<ChatThread> ChatThreads { get; set; } = new List<ChatThread>();

    public virtual ICollection<AccountUser> UsersWithAdmins { get; set; } = new List<AccountUser>();
}
