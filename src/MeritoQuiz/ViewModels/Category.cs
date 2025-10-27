using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeritoQuiz.ViewModels;

public class Category : INotifyPropertyChanged
{
    private bool _isSelected;
    private string _text = string.Empty;

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