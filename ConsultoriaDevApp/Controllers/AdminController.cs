using ConsultoriaDevApp.Data;
using ConsultoriaDevApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultoriaDevApp.Controllers
{
    [Authorize(Policy = "TechLiderOuAdmin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var solicitacoes = await _db.Solicitacoes
                .Include(s => s.Servico)
                .Include(s => s.Dev)
                .OrderByDescending(s => s.CriadaEm)
                .ToListAsync();

            return View(solicitacoes);
        }

        public async Task<IActionResult> Detalhe(int id)
        {
            var solicitacao = await _db.Solicitacoes
                .Include(s => s.Servico)
                .Include(s => s.Dev)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (solicitacao == null)
                return NotFound();

            ViewBag.Devs = await _db.Usuarios
                .Where(u => u.Role == RoleUsuario.Dev && u.Ativo)
                .ToListAsync();

            return View(solicitacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Aprovar(int id, int? devId)
        {
            var solicitacao = await _db.Solicitacoes.FindAsync(id);
            if (solicitacao == null) return NotFound();

            solicitacao.Status = StatusSolicitacao.Aprovada;
            solicitacao.AtualizadaEm = DateTime.UtcNow;
            solicitacao.DevId = devId;

            await _db.SaveChangesAsync();

            TempData["Sucesso"] = $"Solicitação #{id} aprovada com sucesso.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reprovar(int id)
        {
            var solicitacao = await _db.Solicitacoes.FindAsync(id);
            if (solicitacao == null) return NotFound();

            solicitacao.Status = StatusSolicitacao.Reprovada;
            solicitacao.AtualizadaEm = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            TempData["Erro"] = $"Solicitação #{id} reprovada.";
            return RedirectToAction("Index");
        }
    }
}