using ConsultoriaDevApp.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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

            // Relação Solicitacao → Dev sem cascade delete
            modelBuilder.Entity<Solicitacao>()
                .HasOne(s => s.Dev)
                .WithMany()
                .HasForeignKey(s => s.DevId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed de serviços
            modelBuilder.Entity<Servico>().HasData(
                new Servico { Id = 1, Nome = "Consultoria de Arquitetura", Descricao = "Revisão e planejamento da arquitetura do seu sistema.", TempoResposta = "48h", Preco = 500m, Ativo = true },
                new Servico { Id = 2, Nome = "Code Review", Descricao = "Análise completa do código com relatório de melhorias.", TempoResposta = "24h", Preco = 250m, Ativo = true },
                new Servico { Id = 3, Nome = "Desenvolvimento de Feature", Descricao = "Implementação de funcionalidade sob demanda.", TempoResposta = "72h", Preco = 800m, Ativo = true },
                new Servico { Id = 4, Nome = "Correção de Bug Crítico", Descricao = "Diagnóstico e correção de bugs em produção.", TempoResposta = "12h", Preco = 350m, Ativo = true }
            );

            // Seed de usuários
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Tech Líder",
                    Email = "techlider@consultoria.com",
                    SenhaHash = "$2a$11$T1OPZcb7ZdSkQlcFq.Lxw.deUMxKxOX0dOJPRtgGDei5OfLI72TTS",
                    Role = RoleUsuario.TechLider,
                    Ativo = true,
                    CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Admin",
                    Email = "admin@consultoria.com",
                    SenhaHash = "$2a$11$pu1AVkBrO5UnKu1.AbWRf.wdh1YRjz1PLw8OuxAkXPIvsMcaXfa8K",
                    Role = RoleUsuario.Admin,
                    Ativo = true,
                    CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Usuario { Id = 3, Nome = "João Miguel", Email = "joaomiguel@consultoria.com", SenhaHash = "$2a$11$T1OPZcb7ZdSkQlcFq.Lxw.deUMxKxOX0dOJPRtgGDei5OfLI72TTS", Role = RoleUsuario.Dev, Ativo = true, CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Usuario { Id = 4, Nome = "Ana Alice", Email = "anaalice@consultoria.com", SenhaHash = "$2a$11$T1OPZcb7ZdSkQlcFq.Lxw.deUMxKxOX0dOJPRtgGDei5OfLI72TTS", Role = RoleUsuario.Dev, Ativo = true, CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Usuario { Id = 5, Nome = "Bento Oliveira", Email = "bentooliveira@consultoria.com", SenhaHash = "$2a$11$T1OPZcb7ZdSkQlcFq.Lxw.deUMxKxOX0dOJPRtgGDei5OfLI72TTS", Role = RoleUsuario.Dev, Ativo = true, CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Usuario { Id = 6, Nome = "Catarina Roberta", Email = "catarinaroberta@consultoria.com", SenhaHash = "$2a$11$T1OPZcb7ZdSkQlcFq.Lxw.deUMxKxOX0dOJPRtgGDei5OfLI72TTS", Role = RoleUsuario.Dev, Ativo = true, CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new Usuario { Id = 7, Nome = "Ariel Mercedes", Email = "arielmercedes@consultoria.com", SenhaHash = "$2a$11$T1OPZcb7ZdSkQlcFq.Lxw.deUMxKxOX0dOJPRtgGDei5OfLI72TTS", Role = RoleUsuario.Dev, Ativo = true, CriadoEm = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }

            );
        }
    }
}