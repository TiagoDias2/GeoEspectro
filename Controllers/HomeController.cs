using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeoEspectro.Models;
using Microsoft.EntityFrameworkCore;
using GeoEspectro.Data;

namespace GeoEspectro.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index(string? categoria, string? q)
    {
        var artigos = _context.Artigos
            .Include(a => a.ListaRecursos)
            .Where(a =>
                (string.IsNullOrEmpty(q) || a.Titulo.Contains(q)) &&
                (string.IsNullOrEmpty(categoria) || a.ListaCategorias.Any(c => c.Categoria.Categoria == categoria))
            )
            .OrderByDescending(a => a.Data)
            .ToList();

        return View(artigos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
