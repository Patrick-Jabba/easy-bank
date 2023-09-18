using EasyBank.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyBank.Api.Data.Mappings
{
    public class AReceberMap : IEntityTypeConfiguration<AReceber>
    {
        public void Configure(EntityTypeBuilder<AReceber> builder)
        {
            builder.ToTable("areceber")
                   .HasKey(p => p.Id);

            builder.HasOne(p => p.Usuario)
                   .WithMany()
                   .HasForeignKey(fk => fk.IdUsuario);

            builder.HasOne(p => p.NaturezaDeLancamento)
                   .WithMany()
                   .HasForeignKey(fk => fk.IdNaturezaDeLancamento);

            builder.Property(p => p.Descricao)
                   .HasColumnType("VARCHAR")
                   .IsRequired();

            builder.Property(p => p.ValorOriginal)
                   .HasColumnType("double precision")
                   .IsRequired();

            builder.Property(p => p.ValorRecebido)
                   .HasColumnType("double precision")
                   .IsRequired();

            builder.Property(p => p.Observacao)
                   .HasColumnType("VARCHAR");

            builder.Property(p => p.DataCadastro)
                   .HasColumnType("timestamp")
                   .IsRequired();

            builder.Property(p => p.DataVencimento)
                   .HasColumnType("timestamp")
                   .IsRequired();

            builder.Property(p => p.DataReferencia)
                   .HasColumnType("timestamp");

            builder.Property(p => p.DataRecebimento)
                   .HasColumnType("timestamp");

            builder.Property(p => p.DataInativacao)
                   .HasColumnType("timestamp");
        }
    }
}