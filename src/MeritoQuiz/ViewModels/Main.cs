using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MeritoQuiz.Services;

namespace MeritoQuiz.ViewModels;

public class Main
{
    private const int QuestionCount = 2;
    
    public ICommand OptionSelectedCommand { get; }

    public ObservableCollection<Models.Category> Categories { get; } = [];
    
    private readonly QuestionService _questionService;
    private readonly QuizService _quizService;

    public Main(QuestionService questionService, QuizService quizService)
    {
        OptionSelectedCommand = new Command<object>(OnOptionSelected);
        
        _questionService = questionService;
        _quizService = quizService;
    }
    
    public async Task LoadAsync()
    {
        var category = await _questionService.GetCategories();

        Categories.Clear();

        foreach (var c in category)
        {
            Categories.Add(c);
        }
    }

    private async void OnOptionSelected(object obj)
    {
        var category = (obj as Models.Category);
        var questions = category!.Questions
            .OrderBy(x => Random.Shared.Next())
            .Take(QuestionCount);
        
        _quizService.Begin(questions);
        
        await Shell.Current.GoToAsync(nameof(Question));
    }
}