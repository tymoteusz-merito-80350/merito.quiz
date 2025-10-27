using MeritoQuiz.Models;

namespace MeritoQuiz.Services;

public class QuizService
{
    public int Index { get; private set; } = 0;
    public int Count { get => _questions.Count; }

    private List<Question> _questions = [];

    public void Begin(IEnumerable<Question> questions)
    {
        _questions = new List<Question>(questions);
        Index = 0;
    }

    public Question? Get()
    {
        return Index < _questions.Count
            ? _questions[Index]
            : null;
    }

    public bool Check(IEnumerable<string> answers)
    {
        var question = Get();

        if (question is null)
            return true;
        
        var correctAnswers = question.Answers
            .Where(a => a.IsCorrect)
            .ToList();
        var incorrectAnswers = question.Answers
            .Where(a => !a.IsCorrect)
            .ToList();

        return !incorrectAnswers.Any(a => answers.Contains(a.Text)) 
            && correctAnswers.All(a => answers.Contains(a.Text));
    }

    public void Next()
    {
        Index++;
    }
}