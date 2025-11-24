using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MeritoQuiz.Services;

namespace MeritoQuiz.ViewModels;

public class Question : INotifyPropertyChanged
{
    public string QuestionTitle
    {
        get => _questionTitle;
        set
        {
            if (_questionTitle == value)
                return;

            _questionTitle = value;
            OnPropertyChanged();
        }
    }

    public string QuestionText
    {
        get => _questionText;
        set
        {
            if (_questionText == value)
                return;

            _questionText = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Answer> Options { get; } = [];

    public ICommand CheckAnswerCommand { get; }

    private readonly QuizService _quizService;
    private readonly IPopupService? _popupService;

    private string _questionTitle = string.Empty;
    private string _questionText = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public Question(QuizService quizService)
    {
        _quizService = quizService;
        CheckAnswerCommand = new Command(async () => await OnCheckAnswerAsync());
    }

    public Question(QuizService quizService, IPopupService popupService) : this(quizService)
    {
        _popupService = popupService;
    }

    public async Task LoadAsync()
    {
        var question = _quizService.Get();

        if (question is null)
        {
            await Shell.Current.GoToAsync(nameof(Pages.Result), animate: true);
            return;
        }

        QuestionTitle = $"Pytanie {_quizService.Index + 1} / {_quizService.Count}";
        QuestionText = question.Text;
        SetOptions(question.Answers);
    }

    private async Task OnCheckAnswerAsync()
    {
        var isCorrect = _quizService.Check(GetCheckedOptions());
        if (!isCorrect)
        {
            _popupService?.Show("Nieprawidłowa odpowiedź.");
            return;
        }

        _quizService.Next();
        await Shell.Current.GoToAsync(nameof(Pages.Question), animate: true);
    }

    public void SetOptions(IEnumerable<Models.Answer> answers)
    {
        Options.Clear();

        foreach (var answer in answers)
        {
            Options.Add(new Answer
            {
                Text = answer.Text,
            });
        }
    }

    public IEnumerable<string> GetCheckedOptions()
    {
        return Options.Where(o => o.IsSelected).Select(o => o.Text);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}