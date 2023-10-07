using System.ComponentModel.DataAnnotations;
namespace ServiceDesk;

    public class Dispositivo
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }

        public int? UsuarioId { get; set; } // Esta propriedade representa a rela��o
        public Usuario? Usuario { get; set; }
    }
