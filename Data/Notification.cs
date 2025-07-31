using System;
using System.Collections.Generic;

namespace Money_Management.Data;

public partial class Notification
{
    public int Idnotification { get; set; }

    public int? Iduser { get; set; }

    public int? Idexpense { get; set; }

    public string Noidung { get; set; } = null!;

    public DateTime? Ngaytao { get; set; }

    public virtual Expense? IdexpenseNavigation { get; set; }

    public virtual User? IduserNavigation { get; set; }
}
