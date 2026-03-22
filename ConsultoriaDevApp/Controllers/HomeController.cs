using ConsultoriaDevApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultoriaDevApp.Controllers
{
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

            return View(servicos);
        }
    }
}