using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Usuario
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    [Display(Name ="Dirección")]
    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    [Display(Name ="Correo Electronico")]
    public string CorreoElectronico { get; set; } = null!;

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
