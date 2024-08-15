using System;
using System.Collections.Generic;

namespace NEA_Main.Models;

public partial class ChatThread
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? GroupChatId { get; set; }

    public virtual GroupChat? GroupChat { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
