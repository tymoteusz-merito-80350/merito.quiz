using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeritoQuiz.Pages;

public partial class Result : ContentPage
{
    public Result()
    {
        InitializeComponent();
    }

    private async void ReturnButton_OnClicked(object? sender, EventArgs e)
    {
        // Animate back to the root (Main) page
        await Shell.Current.Navigation.PopToRootAsync(animated: true);
    }
}