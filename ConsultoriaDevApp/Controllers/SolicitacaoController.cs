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
        public async Task<IActionResult> Criar(int ServicoId, string NomeCliente, string EmailCliente, string? Observacao)
        {
            var servico = await _db.Servicos.FindAsync(ServicoId);
            if (servico == null)
                return RedirectToAction("Index", "Home");

            var solicitacao = new Solicitacao
            {
                ServicoId = ServicoId,
                NomeCliente = NomeCliente,
                EmailCliente = EmailCliente,
                Observacao = Observacao,
                Status = StatusSolicitacao.Pendente,
                CriadaEm = DateTime.UtcNow
            };

            _db.Solicitacoes.Add(solicitacao);
            await _db.SaveChangesAsync();

            TempData["Mensagem"] = "Solicitação enviada com sucesso! Em breve entraremos em contato.";
            return RedirectToAction("Confirmacao");
        }

        public IActionResult Confirmacao()
        {
            return View();
        }
    }
}