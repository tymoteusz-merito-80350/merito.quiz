using System.ComponentModel.DataAnnotations;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(32)]
    public string Name { get; set; } = null!;

    [MaxLength(32)]
    public string Icon { get; set; } = null!;

    public List<Question> Questions { get; set; } = [];
}
