using System.ComponentModel.DataAnnotations;

namespace ConsultoriaDevApp.Models
{
    public enum StatusSolicitacao
    {
        Pendente,
        Aprovada,
        Reprovada,
        EmAndamento,
        Concluida
    }

    public class Solicitacao
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string NomeCliente { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        [EmailAddress]
        public string EmailCliente { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Observacao { get; set; }

        public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Pendente;

        public DateTime CriadaEm { get; set; } = DateTime.UtcNow;

        public DateTime? AtualizadaEm { get; set; }

        // FK Servico
        public int ServicoId { get; set; }
        public Servico Servico { get; set; } = null!;

        // FK opcional — Dev atribuído após aprovação
        public int? DevId { get; set; }
        public Usuario? Dev { get; set; }
    }
}