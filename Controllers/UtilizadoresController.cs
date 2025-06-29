using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;
using GeoEspectro.Models;
using Microsoft.AspNetCore.Identity;
using GeoEspectro.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GeoEspectro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UtilizadoresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UtilizadoresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        // GET: Utilizadores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Utilizadores.ToListAsync());
        }

        // GET: Utilizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadores = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            return View(utilizadores);
        }

        // GET: Utilizadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Morada,CodPostal,Pais,Nif,Telemovel")] Utilizadores utilizadores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilizadores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilizadores);
        }

        // GET: Utilizadores/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id) // Alterado de string para int
        {
            // Primeiro encontre o Utilizadores (seu modelo)
            var utilizador = await _context.Utilizadores.FindAsync(id);
            if (utilizador == null)
            {
                return NotFound();
            }

            // Depois encontre o ApplicationUser associado
            var user = await _userManager.FindByIdAsync(utilizador.IdentityUserId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UtilizadorEditDTO
            {
                ID = utilizador.ID, // Usando o ID do Utilizadores (int)
                Nome = utilizador.Nome,
                Morada = utilizador.Morada,
                CodPostal = utilizador.CodPostal,
                Pais = utilizador.Pais,
                Nif = utilizador.Nif,
                Telemovel = utilizador.Telemovel,
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            };

            return View(model);
        }

        // POST: Utilizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UtilizadorEditDTO model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Atualize primeiro o Utilizadores
                    var utilizador = await _context.Utilizadores.FindAsync(id);
                    if (utilizador == null)
                    {
                        return NotFound();
                    }

                    // Atualize TODOS os campos do modelo
                    utilizador.Nome = model.Nome;
                    utilizador.Morada = model.Morada;
                    utilizador.CodPostal = model.CodPostal;
                    utilizador.Pais = model.Pais;
                    utilizador.Nif = model.Nif;
                    utilizador.Telemovel = model.Telemovel;

                    _context.Update(utilizador);
                    await _context.SaveChangesAsync();

                    // Depois atualize o ApplicationUser (Identity)
                    var user = await _userManager.FindByIdAsync(utilizador.IdentityUserId);
                    if (user != null)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                        var result = IdentityResult.Success;

                        if (model.IsAdmin && !isAdmin)
                        {
                            result = await _userManager.AddToRoleAsync(user, "Admin");
                        }
                        else if (!model.IsAdmin && isAdmin)
                        {
                            result = await _userManager.RemoveFromRoleAsync(user, "Admin");
                        }

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(model);
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadoresExists(model.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(model);
        }

        // GET: Utilizadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadores = await _context.Utilizadores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            return View(utilizadores);
        }

        // POST: Utilizadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizadores = await _context.Utilizadores.FindAsync(id);
            if (utilizadores != null)
            {
                _context.Utilizadores.Remove(utilizadores);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadoresExists(int id)
        {
            return _context.Utilizadores.Any(e => e.ID == id);
        }
    }
}
