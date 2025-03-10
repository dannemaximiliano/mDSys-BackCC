using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Back_CC.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MovimientosLog> MovimientosLogs { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Saldo> Saldos { get; set; }

    public virtual DbSet<TipoOperacion> TipoOperacions { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=MxRyzen5\\MXRYZEN5;Database=mDsys_cuentaCorriente;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MovimientosLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC07EAF6BF19");

            entity.ToTable("MovimientosLog", tb => tb.HasTrigger("trg_ActualizarSaldo"));

            entity.Property(e => e.FechaOperacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Importe).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Observacion).HasMaxLength(255);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.MovimientosLogs)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__IdPer__5070F446");

            entity.HasOne(d => d.IdTipoOperacionNavigation).WithMany(p => p.MovimientosLogs)
                .HasForeignKey(d => d.IdTipoOperacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__IdTip__5165187F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.MovimientosLogs)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__IdUsu__52593CB8");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC07B75BB816");

            entity.HasIndex(e => e.Email, "UQ__Personas__A9D10534FA7D0ADD").IsUnique();

            entity.HasIndex(e => e.Dni, "UQ__Personas__C035B8DD554FD4BC").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("DNI");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Saldo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Saldos__3214EC0708725D67");

            entity.HasIndex(e => e.IdPersona, "UQ__Saldos__2EC8D2AD87F03E82").IsUnique();

            entity.Property(e => e.FechaUltimaAct)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Saldo1)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("Saldo");

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Saldo)
                .HasForeignKey<Saldo>(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Saldos__IdPerson__4BAC3F29");
        });

        modelBuilder.Entity<TipoOperacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoOper__3214EC07F94CAC0E");

            entity.ToTable("TipoOperacion");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoUsua__3214EC07589FE9E3");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07A9DE9C7F");

            entity.HasIndex(e => e.IdPersona, "UQ__Usuarios__2EC8D2AD7A7AEE29").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Usuarios__536C85E4C31C64E6").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.FechaAlta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaBaja).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.IdPersonaNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__IdPers__44FF419A");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__IdTipo__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
