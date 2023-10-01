using System.Collections.Generic;

namespace ServiceDesk.Models
{
    public class Sla
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Adicione uma coleção de Ticket para representar os Tickets associados a este SLA
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
