using System;
using System.Collections.Generic;

namespace PRACT1718.ModelsBD;

public partial class Student
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string NumberZachotka { get; set; } = null!;

    public bool LiveInObschaga { get; set; }

    public string Groupe { get; set; } = null!;

    public bool Math { get; set; }

    public bool Programming { get; set; }

    public bool History { get; set; }

    public bool Analitic { get; set; }

    public bool English { get; set; }
}
