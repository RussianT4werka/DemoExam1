using System;
using System.Collections.Generic;

namespace DemoExam1.DB;

public partial class Order
{
    public DateTime Datecreation { get; set; }

    public string Orderstatus { get; set; } = null!;

    public string Paymentstatus { get; set; } = null!;

    public string Nameconference { get; set; } = null!;

    public string Equipment { get; set; } = null!;

    public int Amountguests { get; set; }

    public int Orderid { get; set; }

    public virtual ICollection<Orderuserlist> Orderuserlists { get; set; } = new List<Orderuserlist>();
}
