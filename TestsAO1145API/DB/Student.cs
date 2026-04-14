using System;
using System.Collections.Generic;

namespace TestsAO1145API.DB;

public partial class Student
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Age { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();

    public static explicit operator StModel(Student user)
    {
        return new StModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Age = user.Age,
            IdClass = user.IdClass,
            Login = user.Login,
            Password = user.Password,
            Classes = user.Classes,

            Tests = user.Tests

           
        };
    }
}
public partial class StModel
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Age { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? IdClass { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
