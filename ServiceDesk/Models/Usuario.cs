namespace ServiceDesk;

using System.ComponentModel.DataAnnotations;

    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public CentroDeCusto? CentroDeCusto { get; set; }
        public Dispositivo? Dispositivo { get; set; }

        public int? CentroDeCustoId { get; set; }
       
}
