namespace MeritoQuiz.Models;

public class Answer
{
    public int Order { get; set; }
    public string Text { get; set; } = null!;
    public bool IsCorrect { get; set; }
}