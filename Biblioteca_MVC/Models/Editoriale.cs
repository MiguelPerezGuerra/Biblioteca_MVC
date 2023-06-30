using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Editoriale
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    [Display(Name = "Correo Electronico")]
    public string CorreoElectronico { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
