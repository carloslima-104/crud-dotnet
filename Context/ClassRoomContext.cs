using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AplicandoCrud.Entities;
using AplicandoCrud.Controllers;

namespace AplicandoCrud.Context
{
    public class ClassRoomContext : DbContext
    {
       public ClassRoomContext(DbContextOptions<ClassRoomContext> options) : base(options)
       {
       
       }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlunoContext());

            base.OnModelCreating(modelBuilder);
        }
    }
}