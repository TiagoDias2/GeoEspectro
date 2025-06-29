using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;
using Microsoft.AspNetCore.Authorization;

namespace GeoEspectro.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ArtigosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArtigosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Artigos
        /// <summary>
        /// Devolve a lista com todos os Artigos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artigos>>> GetArtigos()
        {
            return await _context.Artigos.ToListAsync();
        }

        // GET: api/Artigos/5
        /// <summary>
        /// Devolver um Artigo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Artigos>> GetArtigos(int id)
        {
            var artigos = await _context.Artigos.FindAsync(id);

            if (artigos == null)
            {
                return NotFound();
            }

            return artigos;
        }

        // PUT: api/Artigos/5
        /// <summary>
        /// Edição de um Artigo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="artigos"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtigos(int id, Artigos artigos)
        {
            if (id != artigos.Id)
            {
                return BadRequest();
            }

            _context.Entry(artigos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtigosExists(id))
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

        // POST: api/Artigos
        /// <summary>
        /// Criação de um novo Artigo
        /// </summary>
        /// <param name="artigos"></param>
        /// <returns></returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Artigos>> PostArtigos(Artigos artigos)
        {
            _context.Artigos.Add(artigos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtigos", new { id = artigos.Id }, artigos);
        }

        // DELETE: api/Artigos/5
        /// <summary>
        /// Eliminação de um Artigo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtigos(int id)
        {
            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos == null)
            {
                return NotFound();
            }

            _context.Artigos.Remove(artigos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtigosExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
