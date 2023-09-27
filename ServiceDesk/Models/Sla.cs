namespace ServiceDesk;

using System.ComponentModel.DataAnnotations;

    public class Sla
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
    }