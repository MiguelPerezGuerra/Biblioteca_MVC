using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Autore
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    [Display(Name ="Fecha Nacimiento")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaNacimiento { get; set; }

    [Display(Name = "Pais Origen")]
    public string PaisOrigen { get; set; } = null!;

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
