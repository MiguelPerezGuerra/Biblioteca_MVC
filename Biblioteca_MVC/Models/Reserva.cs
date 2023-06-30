using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Reserva
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    [Display(Name ="Libro")]
    public int? LibroId { get; set; }

    [Display(Name ="Usuario")]
    public int? UsuarioId { get; set; }

    [Display(Name ="Fecha de Reserva")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode=true)]
    public DateTime FechaReserva { get; set; }

    [Display(Name ="Fecha Vencimiento")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    public DateTime FechaVencimiento { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Libro? Libro { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
