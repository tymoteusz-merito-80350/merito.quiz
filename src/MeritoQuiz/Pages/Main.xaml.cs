namespace MeritoQuiz.Pages;

public partial class Main : ContentPage
{
    public Main(ViewModels.Main viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        _ = viewModel.LoadAsync();
    }

    private async void GeneralButton_OnClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Question));
    }
}