using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ServiceDesk;

    public class CentroDeCusto
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }

}
