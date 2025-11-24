using MeritoQuiz.Shared.DTOs;

namespace MeritoQuiz.Services;

public class QuestionsCache
{
    public DateTime LastSync { get; set; }
    public List<CategoryDto> Categories { get; set; } = [];
}
