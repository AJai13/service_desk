using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServiceDesk.Models
{
    public class Sla
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore]
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
