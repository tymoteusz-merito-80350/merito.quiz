using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeritoQuiz.Backend.Entities;

public class Answer
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }

    public Question? Question { get; set; }

    public int Order { get; set; }

    public string Text { get; set; } = null!;

    public bool IsCorrect { get; set; }
}
