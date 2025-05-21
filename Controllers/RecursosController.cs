using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;
using GeoEspectro.Data.Migrations;

namespace GeoEspectro.Controllers
{
    public class RecursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecursosController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Recursos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recursos.Include(r => r.Autor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recursos = await _context.Recursos
                .Include(r => r.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recursos == null)
            {
                return NotFound();
            }

            return View(recursos);
        }

        // GET: Recursos/Create
        public IActionResult Create()
        {
            ViewData["AutorFK"] = new SelectList(_context.Utilizadores.OrderBy(u => u.Nome), "ID", "Nome");
            return View();
        }

        // POST: Recursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Tipo,Local,Ficheiro,Observacao,AutorFK")] Recursos recurso, IFormFile imagemFoto)
        {
            // vars auxiliares
            bool haErro = false;
            string nomeImagem = "";
            string extensao = Path.GetExtension(imagemFoto.FileName).ToLowerInvariant();
            var extensaoPermitidasMedia = new[] { ".jpeg", ".png", ".mp4", ".ogg", ".webm"};

            if (imagemFoto == null)
            {
                // não há imagem
                haErro = true;
                // crio msg de erro
                ModelState.AddModelError("", "Tem de submeter uma Fotografia");
            }

            else
            {
                if (!extensaoPermitidasMedia.Contains(extensao))
                {
                    // não há imagem
                    haErro = true;
                    // crio msg de erro
                    ModelState.AddModelError("", "Tem de submeter uma Fotografia/Video do tipo indicado");
                }

                else if (imagemFoto.Length > 20 * 1024 * 1024)
                {
                    // não tem a extensão pretendida
                    haErro = true;
                    // crio msg de erro
                    ModelState.AddModelError("", "Não pode submeter ficheiros multimédia superiores a 20 MB");
                }

                else
                {
                    // há imagem,
                    // vamos processá-la
                    //*********************
                    // Novo nome para a imagem
                    Guid g = Guid.NewGuid();
                    nomeImagem = g.ToString();
                    nomeImagem += extensao;

                    // guardar este nome na BD
                    recurso.Ficheiro = nomeImagem;
                }
            }


            if (ModelState.IsValid)
            {
                recurso.Data = DateTime.Now;

                _context.Add(recurso);
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
                await imagemFoto.CopyToAsync(stream);

                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorFK"] = new SelectList(_context.Set<Utilizadores>(), "ID", "ID", recurso.AutorFK);
            return View(recurso);
        }

        // GET: Recursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recursos = await _context.Recursos.FindAsync(id);
            if (recursos == null)
            {
                return NotFound();
            }
            ViewData["AutorFK"] = new SelectList(_context.Set<Utilizadores>(), "ID", "ID", recursos.AutorFK);
            return View(recursos);
        }

        // POST: Recursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Tipo,Local,Observacao,AutorFK")] Recursos recursos)
        {
            if (id != recursos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recursos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursosExists(recursos.Id))
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
            ViewData["AutorFK"] = new SelectList(_context.Set<Utilizadores>(), "ID", "ID", recursos.AutorFK);
            return View(recursos);
        }

        // GET: Recursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recursos = await _context.Recursos
                .Include(r => r.Autor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recursos == null)
            {
                return NotFound();
            }

            return View(recursos);
        }

        // POST: Recursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recursos = await _context.Recursos.FindAsync(id);
            if (recursos != null)
            {
                _context.Recursos.Remove(recursos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursosExists(int id)
        {
            return _context.Recursos.Any(e => e.Id == id);
        }
    }
}
