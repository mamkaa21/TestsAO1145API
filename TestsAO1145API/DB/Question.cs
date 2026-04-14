using System;
using System.Collections.Generic;

namespace TestsAO1145API.DB;

public partial class Question
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Answer { get; set; }

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
