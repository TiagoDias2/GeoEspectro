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
using Microsoft.AspNetCore.Authorization;


namespace GeoEspectro.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index(string searchString, string? categoriaId)
        {
            var query = _context.Artigos
                .Include(a => a.Autor) 
                .Include(a => a.ListaCategorias)
                    .ThenInclude(ac => ac.Categoria)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Titulo.Contains(searchString));
            }

            // Fazemos o parse apenas para o filtro, mas não usamos como binding no DTO
            if (!string.IsNullOrEmpty(categoriaId) && int.TryParse(categoriaId, out int catId))
            {
                query = query.Where(a => a.ListaCategorias.Any(ac => ac.CategoriaId == catId));
            }

            var viewModel = new ArtigosIndexDTO
            {
                SearchString = searchString,
                CategoriaId = categoriaId, // Mantemos como string aqui, como veio da URL
                Artigos = await query.ToListAsync(),
                Categorias = await _context.Categorias
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Categoria
                    })
                    .ToListAsync()
            };

            return View(viewModel);
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
                    .ThenInclude(ac => ac.Categoria)
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

            // Obter o ID do utilizador autenticado
            var utilizadorId = _context.Utilizadores
                .Where(u => u.UserName == User.Identity.Name)
                .Select(u => u.ID)
                .FirstOrDefault();

            var artigo = new Artigos
            {
                Titulo = viewModel.Titulo,
                Texto = viewModel.Texto,
                AutorFK = utilizadorId,
                Data = DateTime.Now,
                ListaCategorias = viewModel.ListaCategoriasSelecionadas
                    .Select(catId => new ArtigosCategoria
                    {
                        CategoriaId = catId
                    }).ToList()
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

            var artigo = await _context.Artigos
                .Include(a => a.ListaCategorias)
                .Include(a => a.ListaRecursos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (artigo == null)
            {
                return NotFound();
            }

            var viewModel = new ArtigoDTO
            {
                Id = artigo.Id,
                Titulo = artigo.Titulo,
                Texto = artigo.Texto,
                UtilizadorFK = artigo.AutorFK,
                ListaCategoriasSelecionadas = artigo.ListaCategorias.Select(ac => ac.CategoriaId).ToList(),
                ListaRecursosSelecionados = artigo.ListaRecursos.Select(d => d.RecursoFK).ToList(),
                ListaUtilizadores = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "ID", "Nome", artigo.AutorFK),
                ListaCategorias = new MultiSelectList(_context.Categorias.OrderBy(c => c.Categoria), "Id", "Categoria"),
                ListaRecursos = new MultiSelectList(_context.Recursos.OrderBy(r => r.Nome), "Id", "Nome")
            };

            return View(viewModel);
        }

        // POST: Artigos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtigoDTO viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel.ListaUtilizadores = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "ID", "Nome", viewModel.UtilizadorFK);
                viewModel.ListaCategorias = new MultiSelectList(_context.Categorias.OrderBy(c => c.Categoria), "Id", "Categoria", viewModel.ListaCategoriasSelecionadas);
                viewModel.ListaRecursos = new MultiSelectList(_context.Recursos.OrderBy(r => r.Nome), "Id", "Nome", viewModel.ListaRecursosSelecionados);
                return View(viewModel);
            }

            var artigo = await _context.Artigos
                .Include(a => a.ListaCategorias)
                .Include(a => a.ListaRecursos)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (artigo == null)
            {
                return NotFound();
            }

            // Atualizar propriedades básicas
            artigo.Titulo = viewModel.Titulo;
            artigo.Texto = viewModel.Texto;
            artigo.Data = DateTime.Now;
            artigo.AutorFK = viewModel.UtilizadorFK;

            // Atualizar categorias
            var categoriasAtuais = artigo.ListaCategorias.Select(ac => ac.CategoriaId).ToList();
            var categoriasParaAdicionar = viewModel.ListaCategoriasSelecionadas.Except(categoriasAtuais).ToList();
            var categoriasParaRemover = categoriasAtuais.Except(viewModel.ListaCategoriasSelecionadas).ToList();

            foreach (var catId in categoriasParaRemover)
            {
                var ac = artigo.ListaCategorias.FirstOrDefault(ac => ac.CategoriaId == catId);
                if (ac != null)
                {
                    artigo.ListaCategorias.Remove(ac);
                }
            }

            foreach (var catId in categoriasParaAdicionar)
            {
                artigo.ListaCategorias.Add(new ArtigosCategoria { CategoriaId = catId });
            }

            // Atualizar recursos
            var recursosAtuais = artigo.ListaRecursos.Select(d => d.RecursoFK).ToList();
            var recursosParaAdicionar = viewModel.ListaRecursosSelecionados.Except(recursosAtuais).ToList();
            var recursosParaRemover = recursosAtuais.Except(viewModel.ListaRecursosSelecionados).ToList();

            foreach (var recursoId in recursosParaRemover)
            {
                var detalhe = artigo.ListaRecursos.FirstOrDefault(d => d.RecursoFK == recursoId);
                if (detalhe != null)
                {
                    _context.Detalhes.Remove(detalhe);
                }
            }

            foreach (var recursoId in recursosParaAdicionar)
            {
                artigo.ListaRecursos.Add(new Detalhes
                {
                    RecursoFK = recursoId,
                    Principal = false // Ou implemente lógica para definir o principal
                });
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtigosExists(artigo.Id))
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
