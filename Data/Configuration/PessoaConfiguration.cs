using System;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidencial.Data.Configuration;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("Pessoas");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nome)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(p => p.Idade)
            .IsRequired();

    }
}
