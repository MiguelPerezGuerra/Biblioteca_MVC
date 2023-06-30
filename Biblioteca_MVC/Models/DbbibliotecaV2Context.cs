using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_MVC.Models;

public partial class DbbibliotecaV2Context : DbContext
{
    public DbbibliotecaV2Context()
    {
    }

    public DbbibliotecaV2Context(DbContextOptions<DbbibliotecaV2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Devolucione> Devoluciones { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Ejemplare> Ejemplares { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Ubicacione> Ubicaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Autores__3214EC2756B7A874");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(255);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.PaisOrigen).HasMaxLength(255);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC27D473C94F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Devolucione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devoluci__3214EC27FB40E3AA");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("TR_Actualizar_Disponiblidad_Devolucion");
                    tb.HasTrigger("TR_Actualizar_Disponiblidad_Prestamos");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaDevolucion).HasColumnType("date");
            entity.Property(e => e.FechaPrestamo).HasColumnType("date");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Libro).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK__Devolucio__Libro__52593CB8");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Devoluciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Devolucio__Usuar__534D60F1");
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Editoria__3214EC275966B15F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CorreoElectronico).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Ejemplare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ejemplar__3214EC27CBC034AF");

            entity.ToTable(tb => tb.HasTrigger("TR_Estado_Ejemplares"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.UbicacionId).HasColumnName("UbicacionID");

            entity.HasOne(d => d.Libro).WithMany(p => p.Ejemplares)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK__Ejemplare__Libro__571DF1D5");

            entity.HasOne(d => d.Ubicacion).WithMany(p => p.Ejemplares)
                .HasForeignKey(d => d.UbicacionId)
                .HasConstraintName("FK__Ejemplare__Ubica__5812160E");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Generos__3214EC275F338673");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libros__3214EC2721256EB7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.AutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Autor)
                .HasConstraintName("FK__Libros__Autor__46E78A0C");

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Categoria)
                .HasConstraintName("FK__Libros__Categori__49C3F6B7");

            entity.HasOne(d => d.EditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Editorial)
                .HasConstraintName("FK__Libros__Editoria__47DBAE45");

            entity.HasOne(d => d.GeneroNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.Genero)
                .HasConstraintName("FK__Libros__Genero__48CFD27E");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personal__3214EC27E26EE4E7");

            entity.ToTable("Personal");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cargo).HasMaxLength(255);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Personals)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Personal__Usuari__440B1D61");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prestamo__3214EC2771C09BCE");

            entity.ToTable(tb => tb.HasTrigger("TR_Prestamo_Deleted"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaLimiteDevolucion).HasColumnType("date");
            entity.Property(e => e.FechaPrestamo).HasColumnType("date");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Libro).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK__Prestamos__Libro__4D94879B");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Prestamos__Usuar__4E88ABD4");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservas__3214EC27AE600410");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.FechaReserva).HasColumnType("date");
            entity.Property(e => e.FechaVencimiento).HasColumnType("date");
            entity.Property(e => e.LibroId).HasColumnName("LibroID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Libro).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("FK__Reservas__LibroI__5BE2A6F2");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Reservas__Usuari__5CD6CB2B");
        });

        modelBuilder.Entity<Ubicacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ubicacio__3214EC274EBD1A7E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estante).HasMaxLength(20);
            entity.Property(e => e.Seccion).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC27B59C1EAB");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(255);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
