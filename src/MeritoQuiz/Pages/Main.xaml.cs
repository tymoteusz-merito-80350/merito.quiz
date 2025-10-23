namespace MeritoQuiz.Pages;

public partial class Main : ContentPage
{
    public Main()
    {
        InitializeComponent();
    }

    private async void GeneralButton_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Question));
    }
}