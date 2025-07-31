using System;
using System.Collections.Generic;

namespace Money_Management.Data;

public partial class Expense
{
    public int Idexpense { get; set; }

    public int? Iduser { get; set; }

    public int? Tuan { get; set; }

    public DateOnly? Tungay { get; set; }

    public DateOnly? Denngay { get; set; }

    public decimal Tonggia { get; set; }

    public bool? Notification { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual User? IduserNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
