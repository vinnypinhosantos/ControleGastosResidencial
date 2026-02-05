using System;
using ControleGastosResidencial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastosResidencial.Data.Configuration;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes", 
                        t => t.HasCheckConstraint("CK_Transacao_ValorEmCentavos", "ValorEmCentavos > 0"));

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Descricao)
            .HasMaxLength(400)
            .IsRequired();

        builder.OwnsOne(t => t.Valor, v =>
        {
            v.Property(p => p.ValorEmCentavos)
                .HasColumnName("ValorEmCentavos")
                .IsRequired();
        });

        builder.Property(t => t.Tipo)
            .IsRequired();

        builder.HasOne(t => t.Categoria)
            .WithMany(c => c.Transacoes)
            .HasForeignKey(t => t.CategoriaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.PessoaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
