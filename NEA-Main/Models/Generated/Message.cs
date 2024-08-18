using System;
using System.Collections.Generic;

namespace NEA_Main.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int SenderId { get; set; }

    public DateTime DateSent { get; set; }

    public TimeOnly TimeSent { get; set; }

    public int ChatThreadId { get; set; }

    public virtual ChatThread ChatThread { get; set; } = null!;

    public virtual AccountUser Sender { get; set; } = null!;
}
