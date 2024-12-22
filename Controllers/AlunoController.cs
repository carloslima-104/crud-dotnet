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
        //Aplicando Get - Aluno
        [HttpGet("AlunoId/{id}")]
        public async Task<ActionResult<Aluno>> ObterAlunoId(int id)
        {
        // Retorna um erro 400 Bad Request se o ID for inválido
                if (id <= 0)
                {
                    return BadRequest("O ID deve ser um número positivo."); 
                }

                var aluno = await _context.Alunos.FindAsync(id);
        // Retorna um erro caso não encontre o Id
                if (aluno == null)
                {
                    return NotFound("Id não existente");
                }
            return Ok(aluno);
        }
        //Aplicando o post - aluno
        [HttpPost("AdicionarAluno")]
        public async Task<ActionResult<Aluno>> AdicionarAluno(Aluno aluno)
        {
            // Verifica se o email já existe
            var alunoExistente = await _context.Alunos.FirstOrDefaultAsync(a => a.Email == aluno.Email);
            if (alunoExistente != null)
            {
                return BadRequest("Email já cadastrado.");
            }

            // Adiciona o aluno ao banco e salva
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AdicionarAluno), new { id = aluno.Id }, aluno);
        }
        // Aplicando o put por ID
        [HttpPut("AtualizarCadastro/{id}")]
        public async Task<ActionResult> AtualizarCadastro(int id, Aluno alunoAtualizado)
        {
            // Verifique se o ID na URL corresponde ao ID no corpo
            if (id != alunoAtualizado.Id) 
            {
                return BadRequest("O ID na URL não corresponde ao ID no corpo da requisição.");
            }

            try
            {
                var aluno = await _context.Alunos.FindAsync(id);

                if (aluno == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                // Verifica se o email já está cadastrados em outro ID
                if (await _context.Alunos.AnyAsync(a => a.Email == alunoAtualizado.Email && a.Id != id))
                {
                    return Conflict("Este email já está em uso por outro usuário.");
                }

                // Validação dos dados do aluno (DataAnnotations)
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Atualiza as propriedades do aluno existente com os valores do alunoAtualizado
                aluno.Nome = alunoAtualizado.Nome;
                aluno.Email = alunoAtualizado.Email;

                await _context.SaveChangesAsync();

                return Ok("Cadastro atualizado.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ) // Captura exceções mais genéricas
            {
                return StatusCode(500, "Erro interno ao atualizar o cadastro.");
            }
        }
        //Aplicando o delete por Id- aluno
        [HttpDelete("DeletarAlunos/{id}")]
        public async Task<ActionResult> DeletarAluno(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id inválido");
                }

                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

                return Ok("Usuário deletado com sucesso!");
            }
            catch (Exception)
            {

                return StatusCode(500, "Ocorreu um erro interno ao excluir o aluno.");
            }
        }
    }
}