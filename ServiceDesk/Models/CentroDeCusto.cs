using System.ComponentModel.DataAnnotations;
namespace ServiceDesk;

    public class CentroDeCusto
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }

        public Usuario Usuario { get; set; }

}
