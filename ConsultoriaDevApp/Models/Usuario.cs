using System.ComponentModel.DataAnnotations;

namespace ConsultoriaDevApp.Models
{
    public enum RoleUsuario
    {
        Cliente,
        TechLider,
        Dev,
        Admin
    }

    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        public RoleUsuario Role { get; set; } = RoleUsuario.Cliente;

        public DateTime CriadoEm { get; set; }

        public bool Ativo { get; set; }
    }
}