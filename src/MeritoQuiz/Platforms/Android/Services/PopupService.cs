using Android.Widget;

namespace MeritoQuiz.Services;

public class PopupService : IPopupService
{
    public void Show(string message)
    {
        var context = Android.App.Application.Context!;
        Toast.MakeText(context, message, ToastLength.Short)!.Show();
    }
}