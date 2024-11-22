using Api.Render.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Api.Render.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Insumo> Insumos { get; set; }
    }
}
