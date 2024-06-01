using System;
using System.Collections.Generic;

namespace DemoExam1.DB;

public partial class Userrole
{
    public int Userroleid { get; set; }

    public string Namerole { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
