using System;
using System.Threading.Tasks;

namespace MeritoQuiz.Pages;

public partial class Question : ContentPage
{
    private readonly ViewModels.Question _viewModel;

    public Question(ViewModels.Question viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadAsync();

#if ANDROID
        // Ensure any residual platform animation is done
        await Task.Delay(1);
#endif

        var w = Width > 0 ? Width : Application.Current?.MainPage?.Width ?? 0;
        TranslationX = w;
        Opacity = 1;
        await this.TranslateTo(0, 0, 220, Easing.CubicOut);
    }
}