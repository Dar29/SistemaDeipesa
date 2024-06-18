using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Sistema.Entidades.Modelos
{
    public partial class temp : DbContext
    {
        public temp()
            : base("name=temp")
        {
        }

        public virtual DbSet<Tbl_Categoria> Tbl_Categoria { get; set; }
        public virtual DbSet<Tbl_Cliente> Tbl_Cliente { get; set; }
        public virtual DbSet<Tbl_DetalleFactura> Tbl_DetalleFactura { get; set; }
        public virtual DbSet<Tbl_DetalleOrdenCompra> Tbl_DetalleOrdenCompra { get; set; }
        public virtual DbSet<Tbl_EstadoMaterial> Tbl_EstadoMaterial { get; set; }
        public virtual DbSet<Tbl_EstadoOrdenCompra> Tbl_EstadoOrdenCompra { get; set; }
        public virtual DbSet<Tbl_Factura> Tbl_Factura { get; set; }
        public virtual DbSet<Tbl_InventarioTracking> Tbl_InventarioTracking { get; set; }
        public virtual DbSet<Tbl_Material> Tbl_Material { get; set; }
        public virtual DbSet<Tbl_Moneda> Tbl_Moneda { get; set; }
        public virtual DbSet<Tbl_OrdenCompra> Tbl_OrdenCompra { get; set; }
        public virtual DbSet<Tbl_Proveedor> Tbl_Proveedor { get; set; }
        public virtual DbSet<Tbl_TipoMovimientoInventario> Tbl_TipoMovimientoInventario { get; set; }
        public virtual DbSet<Tbl_TipoPagos> Tbl_TipoPagos { get; set; }
        public virtual DbSet<Tbl_Usuario> Tbl_Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tbl_Categoria>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Categoria>()
                .HasMany(e => e.Tbl_Material)
                .WithRequired(e => e.Tbl_Categoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.Identificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.UsuarioGrabacion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .Property(e => e.UsuarioModificacion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Cliente>()
                .HasMany(e => e.Tbl_Factura)
                .WithRequired(e => e.Tbl_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_EstadoMaterial>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_EstadoMaterial>()
                .HasMany(e => e.Tbl_Material)
                .WithRequired(e => e.Tbl_EstadoMaterial)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_EstadoOrdenCompra>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_EstadoOrdenCompra>()
                .HasMany(e => e.Tbl_OrdenCompra)
                .WithRequired(e => e.Tbl_EstadoOrdenCompra)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Factura>()
                .Property(e => e.TipoFactura)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Factura>()
                .HasMany(e => e.Tbl_DetalleFactura)
                .WithRequired(e => e.Tbl_Factura)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_InventarioTracking>()
                .Property(e => e.Observacion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Material>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Material>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Material>()
                .HasMany(e => e.Tbl_DetalleFactura)
                .WithRequired(e => e.Tbl_Material)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Material>()
                .HasMany(e => e.Tbl_DetalleOrdenCompra)
                .WithRequired(e => e.Tbl_Material)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Material>()
                .HasMany(e => e.Tbl_InventarioTracking)
                .WithRequired(e => e.Tbl_Material)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Moneda>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Moneda>()
                .HasMany(e => e.Tbl_Material)
                .WithRequired(e => e.Tbl_Moneda)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_OrdenCompra>()
                .HasMany(e => e.Tbl_DetalleOrdenCompra)
                .WithRequired(e => e.Tbl_OrdenCompra)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Proveedor>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Proveedor>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Proveedor>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Proveedor>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Proveedor>()
                .HasMany(e => e.Tbl_OrdenCompra)
                .WithRequired(e => e.Tbl_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_TipoMovimientoInventario>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_TipoMovimientoInventario>()
                .HasMany(e => e.Tbl_InventarioTracking)
                .WithRequired(e => e.Tbl_TipoMovimientoInventario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_TipoPagos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_TipoPagos>()
                .HasMany(e => e.Tbl_Factura)
                .WithRequired(e => e.Tbl_TipoPagos)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .Property(e => e.Apellidos)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .Property(e => e.Contrasenia)
                .IsUnicode(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .HasMany(e => e.Tbl_Factura)
                .WithRequired(e => e.Tbl_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .HasMany(e => e.Tbl_InventarioTracking)
                .WithRequired(e => e.Tbl_Usuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tbl_Usuario>()
                .HasMany(e => e.Tbl_OrdenCompra)
                .WithRequired(e => e.Tbl_Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
