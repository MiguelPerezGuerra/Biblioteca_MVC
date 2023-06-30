using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Libro
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    [Display(Name = "Año Publicación")]
    public int AnioPublicacion { get; set; }

    [Display(Name = "# Copias")]
    public int NumeroCopias { get; set; }

    public int? Autor { get; set; }

    public int? Editorial { get; set; }

    public int? Genero { get; set; }

    public int? Categoria { get; set; }

    [Display(Name = "Autor")]
    public virtual Autore? AutorNavigation { get; set; }

    [Display(Name = "Categoria")]
    public virtual Categoria? CategoriaNavigation { get; set; }

    public virtual ICollection<Devolucione> Devoluciones { get; set; } = new List<Devolucione>();

    [Display(Name = "Editorial")]
    public virtual Editoriale? EditorialNavigation { get; set; }

    public virtual ICollection<Ejemplare> Ejemplares { get; set; } = new List<Ejemplare>();

    [Display(Name = "Genero")]
    public virtual Genero? GeneroNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
