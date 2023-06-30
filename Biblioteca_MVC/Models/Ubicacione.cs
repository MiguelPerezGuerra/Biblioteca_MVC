using System;
using System.Collections.Generic;

namespace Biblioteca_MVC.Models;

public partial class Ubicacione
{
    public int Id { get; set; }

    public string Seccion { get; set; } = null!;

    public string Estante { get; set; } = null!;

    public virtual ICollection<Ejemplare> Ejemplares { get; set; } = new List<Ejemplare>();
}
