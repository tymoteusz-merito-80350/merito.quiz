using MeritoQuiz.Services;
using Microsoft.Extensions.Logging;

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

        return builder.Build();
    }
}