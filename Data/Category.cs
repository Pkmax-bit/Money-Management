using System;
using System.Collections.Generic;

namespace Money_Management.Data;

public partial class Category
{
    public int Idcategory { get; set; }

    public int? Iduser { get; set; }

    public string Ten { get; set; } = null!;

    public string? Ghichu { get; set; }

    public bool? Kichhoat { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual User? IduserNavigation { get; set; }
}
