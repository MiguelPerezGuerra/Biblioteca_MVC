using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Prestamo
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    [Display(Name ="Libro")]
    public int? LibroId { get; set; }
    [Display(Name ="Usuario")]
    public int? UsuarioId { get; set; }
    [Display(Name ="Fecha Prestamo")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaPrestamo { get; set; }
    [Display(Name ="Fecha Devolución")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaLimiteDevolucion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Libro? Libro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
