﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Render.Data;
using Api.Render.Models;

namespace GestaoInsumosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsumosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InsumosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os insumos cadastrados.
        /// </summary>
        /// <returns>Uma lista de insumos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insumo>>> GetInsumos()
        {
            return await _context.Insumos.ToListAsync();
        }

        /// <summary>
        /// Obtém um insumo específico pelo ID.
        /// </summary>
        /// <param name="id">ID do insumo.</param>
        /// <returns>O insumo correspondente ou 404 se não for encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Insumo>> GetInsumo(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);

            if (insumo == null)
            {
                return NotFound();
            }

            return insumo;
        }

        /// <summary>
        /// Adiciona um novo insumo.
        /// </summary>
        /// <param name="insumo">Dados do insumo a ser criado.</param>
        /// <returns>O insumo criado.</returns>
        [HttpPost]
        public async Task<ActionResult<InsumoRequest>> PostInsumo(InsumoRequest insumo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Gerando um insumo fictício para adicionar no banco de dados
            var insumoRequest = new Insumo
            {
                Nome = insumo.Nome,
                Quantidade = insumo.Quantidade,
                Custo = insumo.Custo
            };

            // Adicionando o insumo no contexto (banco de dados)
            _context.Insumos.Add(insumoRequest);
            await _context.SaveChangesAsync(); // O id é gerado aqui pelo banco

            // Atualizando o insumoRequest com o id gerado pelo banco
            var response = CreatedAtAction("GetInsumo", new { id = insumoRequest.Id }, insumoRequest);

            return response;
        }



        /// <summary>
        /// Atualiza um insumo existente.
        /// </summary>
        /// <param name="id">ID do insumo a ser atualizado.</param>
        /// <param name="insumo">Dados atualizados do insumo.</param>
        /// <returns>NoContent se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumo(int id, InsumoRequest insumo)
        {
            // Validação do 'Nome', 'Quantidade' e 'Custo'
            if (string.IsNullOrEmpty(insumo.Nome))
            {
                return BadRequest(new { message = "O campo 'Nome' é obrigatório." });
            }

            if (insumo.Quantidade <= 0)
            {
                return BadRequest(new { message = "A 'Quantidade' deve ser maior que zero." });
            }

            if (insumo.Custo <= 0)
            {
                return BadRequest(new { message = "O 'Custo' deve ser maior que zero." });
            }

            // Busca o insumo no banco
            var insumoExistente = await _context.Insumos.FindAsync(id);
            if (insumoExistente == null)
            {
                return NotFound(new { message = "Insumo não encontrado." });
            }

            // Atualiza as propriedades do insumo existente com os valores recebidos
            insumoExistente.Nome = insumo.Nome;
            insumoExistente.Quantidade = insumo.Quantidade;
            insumoExistente.Custo = insumo.Custo;

            // Marca a entidade como modificada
            _context.Entry(insumoExistente).State = EntityState.Modified;

            try
            {
                // Tenta salvar as mudanças
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Em caso de exceção de concorrência, verifica se o insumo ainda existe
                if (!InsumoExists(id))
                {
                    return NotFound(new { message = "Insumo não encontrado durante a atualização." });
                }
                else
                {
                    throw;
                }
            }

            // Retorna sucesso (sem conteúdo)
            return NoContent();
        }


        /// <summary>
        /// Exclui um insumo pelo ID.
        /// </summary>
        /// <param name="id">ID do insumo a ser excluído.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsumo(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound();
            }

            _context.Insumos.Remove(insumo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsumoExists(int id)
        {
            return _context.Insumos.Any(e => e.Id == id);
        }
    }
}
