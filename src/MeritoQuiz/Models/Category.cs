namespace MeritoQuiz.Models;

public class Category
{
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;

    public List<Question> Questions { get; set; } = [];
}