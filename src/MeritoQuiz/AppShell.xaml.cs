using MeritoQuiz.Pages;

namespace MeritoQuiz;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(Question), typeof(Question));
        Routing.RegisterRoute(nameof(Result), typeof(Result));
    }
}