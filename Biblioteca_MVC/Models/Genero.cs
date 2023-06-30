using System;
using System.Collections.Generic;

namespace Biblioteca_MVC.Models;

public partial class Genero
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
