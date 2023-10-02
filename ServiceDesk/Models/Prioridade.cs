namespace ServiceDesk;

using System.ComponentModel.DataAnnotations;

    public class Prioridade
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
    }