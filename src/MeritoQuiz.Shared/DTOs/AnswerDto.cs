namespace MeritoQuiz.Shared.DTOs;

public class AnswerDto
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public int Order { get; set; }
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}
