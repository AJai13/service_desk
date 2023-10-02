using ServiceDesk.Migrations;
using ServiceDesk.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Funcionario
{
    [Key]
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? Cargo { get; set; }

    [JsonIgnore]
    public ICollection<ServiceDesk.Models.Ticket>? Ticket { get; set; }
}
