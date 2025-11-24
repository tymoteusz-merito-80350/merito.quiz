using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeritoQuiz.Models;

public class Answer : INotifyPropertyChanged
{
    private string _text = string.Empty;
    private bool _isSelected;

    public int Order { get; set; }

    public string Text
    {
        get => _text;
        set
        {
            if (_text == value)
                return;
            _text = value;
            OnPropertyChanged();
        }
    }

    public bool IsCorrect { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value)
                return;
            _isSelected = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}