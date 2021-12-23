using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpeseEF.EF.Configuration
{
    internal class SpesaConfiguration : IEntityTypeConfiguration<Spesa>
    {
        public void Configure(EntityTypeBuilder<Spesa> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.CategoriaId).IsRequired();
            builder.Property(s => s.Utente).IsRequired();
            builder.Property(s => s.Descrizione).IsRequired();
            builder.Property(s => s.Approvato).IsRequired();
            builder.Property(s => s.Importo).HasColumnType("decimal(5,2)").IsRequired();

            builder.HasOne(s => s.Cat).WithMany(c => c.Spese).HasForeignKey(s => s.CategoriaId);
        }
    }
}
