using System;
using System.Collections.Generic;

namespace Money_Management.Data;

public partial class User
{
    public int Iduser { get; set; }

    public string Hoten { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Sdt { get; set; }

    public string? Hinhanh { get; set; }

    public string Password { get; set; } = null!;

    public string? Random { get; set; }

    public DateTime? Lastlogin { get; set; }

    public bool? Kichhoat { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Relationship> Relationships { get; set; } = new List<Relationship>();
}
