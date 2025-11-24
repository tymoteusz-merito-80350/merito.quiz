namespace MeritoQuiz.Backend.DTOs;

public class QuestionDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime ModifiedAt { get; set; }
    public List<AnswerDto> Answers { get; set; } = [];
}