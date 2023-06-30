using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Ejemplare
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    [Display(Name ="Libro")]
    public int? LibroId { get; set; }

    [Display(Name = "Estante")]
    public int? UbicacionId { get; set; }

    [Display(Name ="Copias Disponibles")]
    public int CopiasDisponibles { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Libro? Libro { get; set; }

    [Display(Name = "Estante")]
    public virtual Ubicacione? Ubicacion { get; set; }
}
