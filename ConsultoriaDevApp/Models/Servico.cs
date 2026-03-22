using System.ComponentModel.DataAnnotations;

namespace ConsultoriaDevApp.Models
{
    public class Servico
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, MaxLength(500)]
        public string Descricao { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string TempoResposta { get; set; } = string.Empty; // Ex: "24h", "48h"

        public decimal Preco { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Solicitacao> Solicitacoes { get; set; } = new List<Solicitacao>();
    }
}