using MeritoQuiz.Backend.Entities;
using MeritoQuiz.Backend.Models;

namespace MeritoQuiz.Backend.Repositories;

public interface IQuestionRepository
{
    Task<List<Category>> GetCategoriesWithModifiedQuestionsAsync(DateTime lastSync, CancellationToken ct = default);
}