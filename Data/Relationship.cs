using System;
using System.Collections.Generic;

namespace Money_Management.Data;

public partial class Relationship
{
    public int Idrelationship { get; set; }

    public int? Iduser { get; set; }

    public string Hoten { get; set; } = null!;

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public string? Relationship1 { get; set; }

    public string? Hinhanh { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual User? IduserNavigation { get; set; }
}
