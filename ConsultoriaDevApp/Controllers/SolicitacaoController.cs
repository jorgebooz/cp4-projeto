using ConsultoriaDevApp.Data;
using ConsultoriaDevApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultoriaDevApp.Controllers
{
    public class SolicitacaoController : Controller
    {
        private readonly AppDbContext _db;

        public SolicitacaoController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Solicitacao model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Home");

            model.CriadaEm = DateTime.UtcNow;
            model.Status = StatusSolicitacao.Pendente;

            _db.Solicitacoes.Add(model);
            await _db.SaveChangesAsync();

            TempData["Mensagem"] = "Solicitação enviada com sucesso! Em breve entraremos em contato.";
            TempData["ServicoId"] = model.ServicoId;

            return RedirectToAction("Confirmacao");
        }

        public IActionResult Confirmacao()
        {
            return View();
        }
    }
}