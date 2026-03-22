using ConsultoriaDevApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsultoriaDevApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed inicial de serviços
            modelBuilder.Entity<Servico>().HasData(
                new Servico { Id = 1, Nome = "Consultoria de Arquitetura", Descricao = "Revisão e planejamento da arquitetura do seu sistema.", TempoResposta = "48h", Preco = 500m },
                new Servico { Id = 2, Nome = "Code Review", Descricao = "Análise completa do código com relatório de melhorias.", TempoResposta = "24h", Preco = 250m },
                new Servico { Id = 3, Nome = "Desenvolvimento de Feature", Descricao = "Implementação de funcionalidade sob demanda.", TempoResposta = "72h", Preco = 800m },
                new Servico { Id = 4, Nome = "Correção de Bug Crítico", Descricao = "Diagnóstico e correção de bugs em produção.", TempoResposta = "12h", Preco = 350m }
            );

            // Configura a relação Solicitacao → Dev sem cascade delete
            modelBuilder.Entity<Solicitacao>()
                .HasOne(s => s.Dev)
                .WithMany()
                .HasForeignKey(s => s.DevId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}