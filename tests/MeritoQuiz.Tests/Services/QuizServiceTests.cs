using MeritoQuiz.Models;
using MeritoQuiz.Services;
using NUnit.Framework;

namespace MeritoQuiz.Tests.Services;

public class QuizServiceTests
{
    private static Question MakeQuestion(string text, params (string text, bool isCorrect)[] answers)
    {
        return new Question
        {
            Text = text,
            Answers = answers.Select(a => new Answer { Text = a.text, IsCorrect = a.isCorrect }).ToList(),
        };
    }

    [Test]
    public void Begin_Sets_Index_And_Loads_Questions()
    {
        var svc = new QuizService();
        Question[] qs = [
            MakeQuestion("Q1", ("A", true)),
            MakeQuestion("Q2", ("B", true)),
        ];

        svc.Begin(qs);

        Assert.That(svc.Index, Is.EqualTo(0));
        Assert.That(svc.Count, Is.EqualTo(2));
        Assert.That(svc.Get()!.Text, Is.EqualTo("Q1"));
    }

    [Test]
    public void Get_Returns_Null_When_Index_Beyond_Count()
    {
        var svc = new QuizService();
        svc.Begin([ MakeQuestion("Q1", ("A", true)) ]);
        svc.Next();
        Assert.That(svc.Get(), Is.Null);
    }

    [Test]
    public void Check_Returns_True_When_No_Question()
    {
        var svc = new QuizService();
        // Not calling Begin means no questions
        var result = svc.Check([ "anything" ]);
        Assert.That(result, Is.True);
    }

    [Test]
    public void Check_Works_For_Single_Correct_Answer()
    {
        var svc = new QuizService();
        svc.Begin([ MakeQuestion("Q1", ("A", true), ("B", false)) ]);

        Assert.Multiple(() =>
        {
            Assert.That(svc.Check(["A"]), Is.True, "Correct selection should pass");
            Assert.That(svc.Check(["B"]), Is.False, "Incorrect selection should fail");
            Assert.That(svc.Check([]), Is.False, "Missing correct answer should fail");
            Assert.That(svc.Check(["A", "B"]), Is.False, "Including any incorrect answer should fail");
        });
    }

    [Test]
    public void Check_Works_For_Multiple_Correct_Answers()
    {
        var svc = new QuizService();
        svc.Begin([ MakeQuestion("Q1", ("A", true), ("B", true), ("C", false)) ]);

        Assert.Multiple(() =>
        {
            Assert.That(svc.Check(["A", "B"]), Is.True);
            Assert.That(svc.Check(["A"]), Is.False, "Missing a correct answer should fail");
            Assert.That(svc.Check(["A", "B", "C"]), Is.False, "Any incorrect answer selected should fail");
        });
    }

    [Test]
    public void Next_Increments_Index()
    {
        var svc = new QuizService();
        svc.Begin([ MakeQuestion("Q1", ("A", true)), MakeQuestion("Q2", ("B", true)) ]);
        Assert.That(svc.Get()!.Text, Is.EqualTo("Q1"));
        svc.Next();
        Assert.That(svc.Get()!.Text, Is.EqualTo("Q2"));
    }
}
