using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime dataAbertura { get; set; }
        public Status? status { get; set; }
        public Prioridade? propriedade { get; set; }
        public Categoria? categoria { get; set; }  
        public Dispositivo? Dispositivo { get; set; }
        public Usuario? usuarioCriador { get; set; }
        public Funcionario? funcionarioResponsavel { get; set; }
    }
}
