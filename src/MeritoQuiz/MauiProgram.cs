using MeritoQuiz.Services;
using Microsoft.Extensions.Logging;
using MeritoQuiz.Shared.DTOs;

namespace MeritoQuiz;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<ViewModels.Main>();
        builder.Services.AddTransient<ViewModels.Question>();
        builder.Services.AddSingleton<QuizService>();
        builder.Services.AddSingleton<QuestionService>();
        builder.Services.AddSingleton<IPopupService, PopupService>();

        var apiBase = Environment.GetEnvironmentVariable("MERITOQUIZ_API_BASE")
                       ?? "http://10.0.2.2:5260";
        
        builder.Services.AddSingleton(new QuizApiOptions { BaseUrl = apiBase });

        return builder.Build();
    }
}