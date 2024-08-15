using System;
using System.Collections.Generic;

namespace NEA_Main.Models;

public partial class AccountUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Bio { get; set; }

    public string? ProfileImageUrl { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<GroupChat> JoinedGroupChats { get; set; } = new List<GroupChat>();
}
