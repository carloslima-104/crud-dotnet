using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AplicandoCrud.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicandoCrud.Context
{
        public class AlunoContext : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            // Primary Key
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Senha)
                .IsRequired()
                .HasMaxLength(100);

            builder.ToTable("Aluno");
            
        }
    }
}