using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;
using GeoEspectro.Models.ViewModels;

namespace GeoEspectro.Controllers
{
    public class ArtigosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArtigosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Artigos
        public async Task<IActionResult> Index(string searchString)
        {
            var artigos = _context.Artigos
                .Include(a => a.Autor)
                .Include(a => a.ListaCategorias)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                artigos = artigos.Where(a => a.Titulo.ToLower().Contains(searchString.ToLower()));
            }

            return View(await artigos.ToListAsync());
        }

        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
                .Include(a => a.ListaCategorias)
                .Include(a => a.Autor)
                .Include(a => a.ListaRecursos)
                    .ThenInclude(d => d.Recurso)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (artigo == null)
            {
                return NotFound();
            }

            return View(artigo);
        }

        // GET: Artigos/Create
        public IActionResult Create()
        {
            var viewModel = new ArtigoDTO
            {
                ListaUtilizadores = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "ID", "Nome"),
                ListaCategorias = new MultiSelectList(_context.Categorias.OrderBy(c => c.Categoria), "Id", "Categoria"),
                ListaRecursos = new MultiSelectList(_context.Recursos.OrderBy(r => r.Nome), "Id", "Nome")
            };

            return View(viewModel);
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtigoDTO viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ListaUtilizadores = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "ID", "Nome", viewModel.UtilizadorFK);
                viewModel.ListaCategorias = new MultiSelectList(_context.Categorias.OrderBy(c => c.Categoria), "Id", "Categoria", viewModel.ListaCategoriasSelecionadas);
                viewModel.ListaRecursos = new MultiSelectList(_context.Recursos.OrderBy(r => r.Nome), "Id", "Titulo", viewModel.ListaRecursosSelecionados);
                return View(viewModel);
            }

            var artigo = new Artigos
            {
                Titulo = viewModel.Titulo,
                Texto = viewModel.Texto,
                AutorFK = viewModel.UtilizadorFK,
                Data = DateTime.Now,
                ListaCategorias = _context.Categorias
                    .Where(c => viewModel.ListaCategoriasSelecionadas.Contains(c.Id))
                    .ToList()
            };

            _context.Artigos.Add(artigo);
            await _context.SaveChangesAsync(); // Garante que artigo.Id existe

            // Adiciona cada recurso selecionado via Detalhes
            foreach (var recursoId in viewModel.ListaRecursosSelecionados)
            {
                var detalhe = new Detalhes
                {
                    ArtigoFK = artigo.Id,
                    RecursoFK = recursoId,
                    Principal = false // ou lógica para definir se é o principal
                };
                _context.Add(detalhe);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Artigos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos == null)
            {
                return NotFound();
            }
            return View(artigos);
        }

        // POST: Artigos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Texto,Data")] Artigos artigos)
        {
            if (id != artigos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artigos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtigosExists(artigos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(artigos);
        }

        // GET: Artigos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigos = await _context.Artigos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artigos == null)
            {
                return NotFound();
            }

            return View(artigos);
        }

        // POST: Artigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artigos = await _context.Artigos.FindAsync(id);
            if (artigos != null)
            {
                _context.Artigos.Remove(artigos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtigosExists(int id)
        {
            return _context.Artigos.Any(e => e.Id == id);
        }
    }
}
