using System;
using System.Collections.Generic;

namespace TestsAO1145API.DB;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? IdTeacher { get; set; }

    public virtual Teacher? IdTeacherNavigation { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
