using System;
using System.Collections.Generic;

namespace NEA_Main.Models;

public partial class AccountUserGroupChat
{
    public int Id { get; set; }

    public int AccountUserId { get; set; }

    public int GroupChatId { get; set; }

    public virtual AccountUser AccountUser { get; set; } = null!;

    public virtual GroupChat GroupChat { get; set; } = null!;
}
