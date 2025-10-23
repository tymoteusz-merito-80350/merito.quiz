using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeritoQuiz.ViewModels;

public class QuestionViewModel : INotifyPropertyChanged
{
    public string QuestionTitle
    {
        get => _questionTitle;
        set
        {
            if (_questionTitle == value) 
                return; 
            
            _questionTitle = value; 
            OnPropertyChanged();
        }
    }

    public string QuestionText
    {
        get => _questionText;
        set
        {
            if (_questionText == value) 
                return; 
            
            _questionText = value; 
            OnPropertyChanged();
        }
    }

    public ObservableCollection<AnswerOption> Options { get; } = [];

    private string _questionTitle = "Pytanie 1/10";
    private string _questionText = "Lorem ipsum dolor sit amet.";

    public QuestionViewModel()
    {
        // Demo data
        Options.Add(new AnswerOption { Text = "Odpowiedź A" });
        Options.Add(new AnswerOption { Text = "Odpowiedź B" });
        Options.Add(new AnswerOption { Text = "Odpowiedź C" });
        Options.Add(new AnswerOption { Text = "Odpowiedź D" });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}