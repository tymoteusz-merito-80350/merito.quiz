using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Entities;

public class Question
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(Category))]
    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public string Text { get; set; } = null!;

    public DateTime ModifiedAt { get; set; }

    public List<Answer> Answers { get; set; } = [];
}
