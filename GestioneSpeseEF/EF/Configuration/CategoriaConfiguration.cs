using GestioneSpeseEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpeseEF.EF.Configuration
{
    internal class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.NomeCategoria).IsRequired();

            builder.HasMany(c => c.Spese).WithOne(s => s.Cat).HasForeignKey(s => s.CategoriaId);
        }
    }
}
