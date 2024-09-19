using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NEA_Main.Models.Generated;

[Index("ChatThreadId", Name = "IX_Messages_ChatThreadId")]
[Index("SenderId", Name = "IX_Messages_SenderId")]
public partial class Message
{
    [Key]
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int SenderId { get; set; }

    public DateTime DateSent { get; set; }

    public TimeOnly TimeSent { get; set; }

    public int ChatThreadId { get; set; }

    public string SenderUsername { get; set; } = null!;

    public string? SenderProfilePictureUrl { get; set; }

    [ForeignKey("ChatThreadId")]
    [InverseProperty("Messages")]
    public virtual ChatThread ChatThread { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("Messages")]
    public virtual AccountUser Sender { get; set; } = null!;
}
