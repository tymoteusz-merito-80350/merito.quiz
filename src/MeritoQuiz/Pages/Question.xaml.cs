using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeritoQuiz.Services;

#if ANDROID
using Android.Widget;
using Android.Content;
#endif

namespace MeritoQuiz.Pages;

public partial class Question : ContentPage
{
    private readonly ViewModels.Question _viewModel = new();
    
    private readonly QuizService _quizService;
    
    public Question(QuizService quizService)
    {
        InitializeComponent();
        
        BindingContext = _viewModel;
        _quizService = quizService;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await DisplayQuestion();

#if ANDROID
        // Ensure any residual platform animation is done
        await Task.Delay(1);
#endif

        var w = Width > 0 ? Width : Application.Current?.MainPage?.Width ?? 0;
        TranslationX = w;
        Opacity = 1;
        await this.TranslateTo(0, 0, 220, Easing.CubicOut);
    }

    private async Task DisplayQuestion()
    {
        var question = _quizService.Get();

        if (question is null)
        {
            await Shell.Current.GoToAsync(nameof(Result), animate: true);
            return;
        }

        _viewModel.QuestionTitle = $"Pytanie {_quizService.Index + 1} / {_quizService.Count}";
        _viewModel.QuestionText = question.Text;
        _viewModel.SetOptions(question.Answers);
    }

    private async void GeneralButton_OnClicked(object? sender, EventArgs e)
    {
        var isCorrect = _quizService.Check(_viewModel.GetCheckedOptions());

        if (!isCorrect)
        {
#if ANDROID
            var context = Android.App.Application.Context!;
            Toast.MakeText(context, "Nieprawidłowa odpowiedź.", ToastLength.Short)!.Show();
#endif
            return;
        }
        
        _quizService.Next();
        await Shell.Current.GoToAsync(nameof(Question), animate: true);
    }
}