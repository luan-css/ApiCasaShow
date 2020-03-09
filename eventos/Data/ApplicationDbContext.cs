using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using eventos.Models;

namespace eventos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Genero> Generos {get; set;}
        public DbSet<Casa> Casas {get; set;}
        public DbSet<Evento> Eventos{get; set;}
        public DbSet<Estoque> Estoques{get; set;}
        public DbSet<Saida> Saidas{get; set;}
        public DbSet<Venda> Vendas{get; set;}
        public DbSet<Compra> Compra{get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
