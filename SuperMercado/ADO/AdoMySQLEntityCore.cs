﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace SuperMercado.ADO
{
    public class AdoMySQLEntityCore : DbContext, IADO
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<HistorialPrecio> HistorialPrecios { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Usar los datos usuario y pass del SGBD de la terminal donde se va a usar
            optionsBuilder.UseMySQL("server=localhost;database=supermercado;user=supermercado;password=supermercado");
        }

        public void agregarCategoria(Categoria categoria)
        {
            Categorias.Add(categoria);
            SaveChanges();
        }

        public void agregarProducto(Producto producto)
        {
            Productos.Add(producto);
            SaveChanges();
        }

        public void actualizarProducto(Producto producto)
        {
            this.Update<Producto>(producto);
            SaveChanges();
        }

        public void agregarTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            SaveChanges();
        }

        public List<Categoria> obtenerCategorias() => Categorias.ToList();

        public List<Producto> obtenerProductos()
        {
            return   Productos
                    .Include(producto => producto.Categoria)
                    .ToList();
        }

        public List<HistorialPrecio> historialDe(Producto producto)
        {
            return   HistorialPrecios
                    .Where(historial => historial.Producto == producto)
                    .ToList();
        }

        public List<Ticket> obtenerTickets() => Tickets.ToList();

        public void actualizarTicket(Ticket ticket)
        {
            this.Update<Ticket>(ticket);
            SaveChanges();
        }
        public List<Item> itemsDe(Ticket ticket)
        {
            return   Items
                    .Where(item => item.Ticket == ticket)
                    .Include(item => item.Producto)
                        .ThenInclude(produ => produ.Categoria)
                    .ToList();
        }        
    }
}