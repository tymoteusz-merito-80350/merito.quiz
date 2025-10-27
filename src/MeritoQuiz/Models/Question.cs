namespace MeritoQuiz.Models;

public class Question
{
    public string Text { get; set; } = null!;
    public List<Answer> Answers { get; set; } = [];
}