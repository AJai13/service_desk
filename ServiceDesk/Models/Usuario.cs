namespace ServiceDesk;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public CentroDeCusto? CentroDeCusto { get; set; }
        public Dispositivo? Dispositivo { get; set; }

        public int? CentroDeCustoId { get; set; }
       
}
