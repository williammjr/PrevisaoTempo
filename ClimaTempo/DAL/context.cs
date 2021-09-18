using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClimaTempo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClimaTempo.DAL
{
    public class context : DbContext
    {
        public context() : base("ClimaTempoSimples")
        {
        }

        public DbSet<PrevisaoClima> PrevisaoClima { get; set; }

        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Estado> Estado { get; set; }
    }
}
