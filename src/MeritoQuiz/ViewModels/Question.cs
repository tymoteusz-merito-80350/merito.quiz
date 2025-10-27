using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeritoQuiz.ViewModels;

public class Question : INotifyPropertyChanged
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

    public ObservableCollection<Answer> Options { get; } = [];

    private string _questionTitle = string.Empty;
    private string _questionText = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void SetOptions(IEnumerable<Models.Answer> answers)
    {
        Options.Clear();
        
        foreach (var answer in answers) 
        {
            Options.Add(new Answer {
                Text = answer.Text,
            }); 
        }
    }
    
    public IEnumerable<string> GetCheckedOptions()
    {
        return Options.Where(o => o.IsSelected).Select(o => o.Text);
    }
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}