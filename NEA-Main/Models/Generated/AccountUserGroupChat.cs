using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NEA_Main.Models.Generated;

[Index("AccountUserId", Name = "IX_AccountUserGroupChats_AccountUserId")]
[Index("GroupChatId", Name = "IX_AccountUserGroupChats_GroupChatId")]
public partial class AccountUserGroupChat
{
    [Key]
    public int Id { get; set; }

    public int AccountUserId { get; set; }

    public int GroupChatId { get; set; }

    [ForeignKey("AccountUserId")]
    [InverseProperty("AccountUserGroupChats")]
    public virtual AccountUser AccountUser { get; set; } = null!;

    [ForeignKey("GroupChatId")]
    [InverseProperty("AccountUserGroupChats")]
    public virtual GroupChat GroupChat { get; set; } = null!;
}
