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
        [JsonIgnore]
        public Status? status { get; set; }
        [JsonIgnore]
        public Prioridade? propriedade { get; set; }
        [JsonIgnore]
        public Categoria? categoria { get; set; }
        [JsonIgnore]
        public Sla? sla { get; set; }
        [JsonIgnore]
        public Dispositivo? Dispositivo { get; set; }
        public Usuario? usuarioCriador { get; set; }
        public Funcionario? funcionarioResponsavel { get; set; }

        [JsonIgnore]
        public int? SolucaoId { get; set; }
        [JsonIgnore]
        public Solucao Solucao { get; set; }

        public int? funcionarioResponsavelId { get; set; }
    }
}
