using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceDesk.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime dataAbertura { get; set; }

        public int? StatusId { get; set; }

        public int? PrioridadeId { get; set; }

        public int? CategoriaId { get; set; }

        public int? DispositivoId { get; set; }

        public int? UsuarioId { get; set; }

        public int? FuncionarioId { get; set; }

        public int? SolucaoId { get; set; }

        public int? SlaId { get; set; }

        [JsonIgnore]
        public Sla? sla { get; set; }
        public Status? status { get; set; }
        public Prioridade? prioridade { get; set; }
        public Categoria? categoria { get; set; }
        public Dispositivo? Dispositivo { get; set; }
        public Usuario? usuarioCriador { get; set; }
        public Funcionario? funcionarioResponsavel { get; set; }
        public Solucao? Solucao { get; set; }
    }
}
