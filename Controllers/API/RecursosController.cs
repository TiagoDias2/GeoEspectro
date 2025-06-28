using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;
using GeoEspectro.Models.ViewModels;

namespace GeoEspectro.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Recursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecursosDTO>>> GetRecursos()
        {
            // O que tínhamos antes:
            // SELECT *
            // FROM Recursos
            // return await _context.Recursos.ToListAsync();

            // O que pretendemos agora:
            // SELECT Id, Nome, Data, Tipo, Local, Observacao
            // FROM Recursos

            var listagemRecursos = await _context.Recursos
                .OrderByDescending(r => r.Data)
                .Select(r => new RecursosDTO
                {
                    Nome = r.Nome,
                    Data = r.Data,
                    Tipo = r.Tipo,
                    Local = r.Local,
                    Observacao = r.Observacao
                })
                .ToListAsync();

            return listagemRecursos;
        }

        // GET: api/Recursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecursosDTO>> GetRecurso(int id)
        {
            var recurso = await _context.Recursos
                .Where(r => r.Id == id)
                .Select(r => new RecursosDTO
                {
                    Nome = r.Nome,
                    Data = r.Data,
                    Tipo = r.Tipo,
                    Local = r.Local,
                    Observacao = r.Observacao
                })
                .FirstOrDefaultAsync();

            if (recurso == null)
            {
                return NotFound();
            }

            return recurso;
        }

        // PUT: api/Recursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecurso(int id, Recursos recurso)
        {
            if (id != recurso.Id)
            {
                return BadRequest();
            }

            _context.Entry(recurso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecursosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recursos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recursos>> PostRecurso(Recursos recurso)
        {
            _context.Recursos.Add(recurso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecursos", new { id = recurso.Id }, recurso);
        }

        // DELETE: api/Recursos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecurso(int id)
        {
            var recurso = await _context.Recursos.FindAsync(id);
            if (recurso == null)
            {
                return NotFound();
            }

            _context.Recursos.Remove(recurso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecursosExists(int id)
        {
            return _context.Recursos.Any(e => e.Id == id);
        }
    }
}
