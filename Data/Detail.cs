using System;
using System.Collections.Generic;

namespace Money_Management.Data;

public partial class Detail
{
    public int Iddetail { get; set; }

    public int? Idexpense { get; set; }

    public DateOnly Ngay { get; set; }

    public TimeOnly? Gio { get; set; }

    public string? Thu { get; set; }

    public int? Idcategory { get; set; }

    public decimal Giatien { get; set; }

    public string? Ghichu { get; set; }

    public int? Idrelationship { get; set; }

    public int? Idpaymen { get; set; }

    public virtual Category? IdcategoryNavigation { get; set; }

    public virtual Expense? IdexpenseNavigation { get; set; }

    public virtual Relationship? IdrelationshipNavigation { get; set; }
}
