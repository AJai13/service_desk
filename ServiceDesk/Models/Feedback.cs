using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Feedback
{
    [Key]
    public int Id { get; set; }

    public string FeedbackText { get; set; }

    [ForeignKey("Solucao")]
    public int SolucaoId { get; set; }

    public Solucao? Solucao { get; set; }
}
