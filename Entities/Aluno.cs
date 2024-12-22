using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicandoCrud.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Senha { get; set; }

    }
}