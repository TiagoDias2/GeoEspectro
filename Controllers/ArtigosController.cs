using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artigos.ToListAsync());
        }

        // GET: Artigos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artigo = await _context.Artigos
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
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "ID", "Nome");
            ViewData["ListaCategorias"] = new MultiSelectList(_context.Categorias.OrderBy(c => c.Categoria), "Id", "Categoria");
            return View();
        }

        // POST: Artigos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Fotografia,Texto, UtilizadorFK, ListaCategoriasSelecionadas")] Artigos artigo,
            IFormFile imagemFoto)
        {
            // vars auxiliares
            bool haErro = false;
            string nomeImagem = "";

            if(artigo.AutorFK <= 0)
            {
                // não há imagem
                haErro = true;
                // crio msg de erro
                ModelState.AddModelError("", "Tem de escolher um utilizador");
            }
            else
            {
                var utilizadorExiste = await _context.Utilizadores.AnyAsync(u => u.ID == artigo.AutorFK);
                if (!utilizadorExiste)
                {
                    haErro = true;
                    ModelState.AddModelError("", "O utilizador selecionado não existe.");
                }
            }

            if (artigo.ListaCategoriasSelecionadas == null || artigo.ListaCategoriasSelecionadas.Count == 0)
            {
                // não escolheu categorias
                haErro = true;
                // apresenta mensagem de erro
                ModelState.AddModelError("", "Tem de escolher pelo menos uma categoria.");
            }
            else
            {
                artigo.ListaCategorias = _context.Categorias
                    .Where(c => artigo.ListaCategoriasSelecionadas.Contains(c.Id))
                    .ToList();
            }

            if (imagemFoto == null)
            {
                // não há imagem
                haErro = true;
                // crio msg de erro
                ModelState.AddModelError("", "Tem de submeter uma Fotografia");
            }

            else
            {
                if(imagemFoto.ContentType != "image/jpeg"
                    && imagemFoto.ContentType != "image/png")
                {
                    // não há imagem
                    haErro = true;
                    // crio msg de erro
                    ModelState.AddModelError("", "Tem de submeter uma Fotografia do tipo indicado");
                }
                else
                {
                    // há imagem,
                    // vamos processá-la
                    //*********************
                    // Novo nome para a imagem
                    Guid g = Guid.NewGuid();
                    nomeImagem = g.ToString();
                    string extensao = Path.GetExtension(imagemFoto.FileName).ToLowerInvariant();
                    nomeImagem += extensao;

                    // guardar este nome na BD
                    artigo.Fotografia = nomeImagem;
                }
            }

            // Avalia se os dados estão de acordo com o Model
            if (ModelState.IsValid && !haErro)
            {
                artigo.Data = DateTime.Now;

                _context.Add(artigo);
                await _context.SaveChangesAsync();

                string localizacaoImagem = _webHostEnvironment.WebRootPath;
                localizacaoImagem = Path.Combine(localizacaoImagem, "imagens");
                if (!Directory.Exists(localizacaoImagem))
                {
                    Directory.CreateDirectory(localizacaoImagem);
                }
                nomeImagem = Path.Combine(localizacaoImagem, nomeImagem);
                using var stream = new FileStream(
                    nomeImagem, FileMode.Create
                    );
                await imagemFoto.CopyToAsync( stream );

                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadorFK"] = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "Id", "Nome");
            ViewData["ListaCategorias"] = new MultiSelectList(_context.Categorias.OrderBy(c => c.Categoria), "Id", "Categoria", artigo.ListaCategorias);

            return View(artigo);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Fotografia,Texto,Data")] Artigos artigos)
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
