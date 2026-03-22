using ConsultoriaDevApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ConsultoriaDevApp.Controllers;


public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var servicos = await _db.Servicos
            .Where(s => s.Ativo)
            .OrderBy(s => s.Nome)
            .ToListAsync();

        var h1 = BCrypt.Net.BCrypt.HashPassword("123456");
        var h2 = BCrypt.Net.BCrypt.HashPassword("admin123");
        var h3 = BCrypt.Net.BCrypt.HashPassword("dev123");
        System.Diagnostics.Debug.WriteLine($"Hash 123456: {h1}");
        System.Diagnostics.Debug.WriteLine($"Hash admin123: {h2}");
        System.Diagnostics.Debug.WriteLine($"Hash dev123: {h3}");

        return View(servicos);


    }
}