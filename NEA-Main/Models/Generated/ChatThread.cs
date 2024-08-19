using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NEA_Main.Models.Generated;

[Index("GroupChatId", Name = "IX_ChatThreads_GroupChatId")]
public partial class ChatThread
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int GroupChatId { get; set; }

    [ForeignKey("GroupChatId")]
    [InverseProperty("ChatThreads")]
    public virtual GroupChat GroupChat { get; set; } = null!;

    [InverseProperty("ChatThread")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
