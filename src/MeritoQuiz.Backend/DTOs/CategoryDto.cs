namespace MeritoQuiz.Backend.DTOs;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = [];
}