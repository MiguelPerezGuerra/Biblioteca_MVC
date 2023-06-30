using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_MVC.Models;

public partial class Personal
{
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public string Cargo { get; set; } = null!;

    [Display(Name ="Usuario")]
    public int? UsuarioId { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
