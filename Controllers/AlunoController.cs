using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AplicandoCrud.Entities;
using AplicandoCrud.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Runtime.CompilerServices;


namespace AplicandoCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AlunoController : ControllerBase
    {
        private readonly ClassRoomContext _context;
        public AlunoController(ClassRoomContext context)
        {
            _context = context;
        }
        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.Id == id);
        }
        //Aplicando Get - Aluno
        [HttpGet("AlunoNome/")]
        public async Task<ActionResult<IEnumerable<Aluno>>> ObterAlunoNome(String nome)
        {
            var alunos = await _context.Alunos.Where(a => a.Nome.Contains(nome)).ToListAsync();
           
            return Ok(alunos);
        }
    }
}