using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NEA_Main.Models.Generated;

public partial class AccountUser
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Bio { get; set; }

    [Column("ProfileImageURL")]
    public string? ProfileImageUrl { get; set; }

    [InverseProperty("AccountUser")]
    public virtual ICollection<AccountUserGroupChat> AccountUserGroupChats { get; set; } = new List<AccountUserGroupChat>();

    [InverseProperty("Sender")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
