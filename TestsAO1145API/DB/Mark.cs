using System;
using System.Collections.Generic;

namespace TestsAO1145API.DB;

public partial class Mark
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
