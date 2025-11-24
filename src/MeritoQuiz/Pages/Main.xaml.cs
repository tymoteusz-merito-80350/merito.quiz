namespace MeritoQuiz.Pages;

public partial class Main : ContentPage
{
    public Main(ViewModels.Main viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        _ = viewModel.LoadAsync();
    }
}