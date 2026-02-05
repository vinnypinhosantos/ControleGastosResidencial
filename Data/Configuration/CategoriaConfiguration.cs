using System;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidencial.Data.Configuration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Descricao)
            .HasMaxLength(400)
            .IsRequired();
        builder.Property(c => c.Finalidade)
            .IsRequired();
    }
}
