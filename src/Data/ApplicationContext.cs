using Efcore.Tips.Domain;
using Efcore.Tips.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efcore.Tips.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Colaborador>? Colaboradores { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }
        public DbSet<UsuarioFuncao>? UsuarioFuncoes { get; set; }
        public DbSet<DepartamentoRelatorio>? DepartamentoRelatorio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Insira a linha de conexao")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        #region SemPrimarykey
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UsuarioFuncao>().HasNoKey();
        //}
        #endregion

        #region UsandoViewsDoBancoDeDados

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DepartamentoRelatorio>(e =>
        //    {
        //        e.HasNoKey();

        //        e.ToView("vw_departamento_relatorio");

        //        e.Property(p => p.Departamento).HasColumnName("Descricao");
        //    });
        //}

        #endregion

        #region ForcandoVARCHAR

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DepartamentoRelatorio>(e =>
        //    {
        //        e.HasNoKey();

        //        e.ToView("vw_departamento_relatorio");

        //        e.Property(p => p.Departamento).HasColumnName("Descricao");
        //    });
        //    var properties = modelBuilder.Model.GetEntityTypes()
        //        .SelectMany(p => p.GetProperties())
        //        .Where(p => p.ClrType == typeof(string)
        //                && p.GetColumnType() == null);

        //    foreach (var property in properties)
        //    {
        //        property.SetIsUnicode(false);
        //    }
        //}

        #endregion

        #region SnakeCaseExtensions

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartamentoRelatorio>(e =>
            {
                e.HasNoKey();

                e.ToView("vw_departamento_relatorio");

                e.Property(p => p.Departamento).HasColumnName("Descricao");
            });
            var properties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(p => p.GetProperties())
                .Where(p => p.ClrType == typeof(string)
                        && p.GetColumnType() == null);

            foreach (var property in properties)
            {
                property.SetIsUnicode(false);
            }


            modelBuilder.ToSnakeCaseNames();
        }

        #endregion

    }
}
