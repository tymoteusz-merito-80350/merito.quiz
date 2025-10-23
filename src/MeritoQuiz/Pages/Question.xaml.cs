using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeritoQuiz.Pages;

using MeritoQuiz.ViewModels;

public partial class Question : ContentPage
{
    public Question()
    {
        InitializeComponent();
        BindingContext = new QuestionViewModel();
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

#if ANDROID
        // Ensure any residual platform animation is done
        await Task.Delay(1);
#endif

        var w = Width > 0 ? Width : Application.Current?.MainPage?.Width ?? 0;
        TranslationX = w;
        Opacity = 1;
        await this.TranslateTo(0, 0, 220, Easing.CubicOut);
    }

    private async void GeneralButton_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Result));
    }
}