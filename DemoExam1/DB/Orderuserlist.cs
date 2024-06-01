using System;
using System.Collections.Generic;

namespace DemoExam1.DB;

public partial class Orderuserlist
{
    public int Orderuserlistid { get; set; }

    public int Userid { get; set; }

    public int Orderid { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
