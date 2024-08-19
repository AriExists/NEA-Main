using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NEA_Main.Models.Generated;

public partial class GroupChat
{
    [Key]
    public int Id { get; set; }

    [Column("JoinID")]
    public int JoinId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [InverseProperty("GroupChat")]
    public virtual ICollection<AccountUserGroupChat> AccountUserGroupChats { get; set; } = new List<AccountUserGroupChat>();

    [InverseProperty("GroupChat")]
    public virtual ICollection<ChatThread> ChatThreads { get; set; } = new List<ChatThread>();
}
